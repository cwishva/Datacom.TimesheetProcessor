
namespace ConsoleApp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.Get<AppSettings>();

            // services
            services.AddTransient<ITimesheetProcessor, CsvTimesheetProcessor>();

            //register delegating handlers
            services.AddTransient<DatacomAuthDelegatingHandler>();

            // policy
            var unauthorizedPolicy = Policy
                .HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.Unauthorized)
                .RetryAsync();

            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            services.AddRestEaseClient<IDatacomService>(settings.API.BaseUrl)
                .SetHandlerLifetime(TimeSpan.FromMinutes(3))
                .AddHttpMessageHandler<DatacomAuthDelegatingHandler>()
                .AddPolicyHandler(unauthorizedPolicy)
                .AddPolicyHandler(retryPolicy);

            return services;
        }
    }
}
