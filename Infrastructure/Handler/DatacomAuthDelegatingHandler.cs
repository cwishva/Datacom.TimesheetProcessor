

namespace Infrastructure.Handler
{
    public class DatacomAuthDelegatingHandler 
        : DelegatingHandler
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AppSettings _appSettings;

        public DatacomAuthDelegatingHandler(
            IHttpClientFactory httpClientFactory, 
            IOptions<AppSettings> options)
        {
            _httpClientFactory = httpClientFactory;
            _appSettings = options.Value;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var auth = request.Headers.Authorization;
            if (auth != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.ConnectionClose = true;

                // Post auth body content
                var values = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials")
                };
                var content = new FormUrlEncodedContent(values);
                var authenticationString = $"{_appSettings.API.ClientId}:{_appSettings.API.Secret}";
                var base64EncodedAuthenticationString = Convert.ToBase64String(Encoding.ASCII.GetBytes(authenticationString));

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, _appSettings.API.AuthUrl);
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
                requestMessage.Content = content;

                // make the auth request
                var clientResponse = await client.SendAsync(requestMessage, cancellationToken);
                clientResponse.EnsureSuccessStatusCode();

                string responseBody = await clientResponse.Content.ReadAsStringAsync(cancellationToken);
                var res = JsonConvert.DeserializeObject<Authtoken>(responseBody);
                request.Headers.Authorization = new AuthenticationHeaderValue(
                                    auth.Scheme,
                                    res.access_token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
