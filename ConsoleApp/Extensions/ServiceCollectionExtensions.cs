
namespace ConsoleApp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.Get<AppSettings>();

            //register delegating handlers
            services.AddTransient<DatacomAuthorizationDelegatingHandler>();

            // policy
            var policy = Policy
                .HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.NotFound)
                .RetryAsync();

            services.AddRestEaseClient<IDatacomService>(settings.API.BaseUrl)
                .SetHandlerLifetime(TimeSpan.FromMinutes(3))
                .AddHttpMessageHandler<DatacomAuthorizationDelegatingHandler>()
                //.AddPolicyHandler(policy)
                .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5)
                }));

            return services;
        }
    }
}
