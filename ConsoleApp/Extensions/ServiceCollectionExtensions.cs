
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
            var policy = Policy
                .HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.Unauthorized)
                .RetryAsync();

            services.AddRestEaseClient<IDatacomService>(settings.API.BaseUrl)
                .SetHandlerLifetime(TimeSpan.FromMinutes(3))
                .AddHttpMessageHandler<DatacomAuthDelegatingHandler>()
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
