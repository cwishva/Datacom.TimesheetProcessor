// Console app register Global exception handler
// If it was a MVC application we can
// Extend IExceptionFilter and register the filter with AddControllers options
AppDomain.CurrentDomain.UnhandledException += GlobalExceptionFilter.TimesheetProcessorExceptionHandler;

// Host caller
var host = CreateHostBuilder(args).Build();
var settings = host.Services.GetService<IOptions<AppSettings>>();
if (
    settings == null ||
    string.IsNullOrEmpty(settings.Value.API.ClientId) ||
    string.IsNullOrEmpty(settings.Value.API.Secret))
{
    throw new TimesheetProcessorException("API ClientId and Secret not available");
}
else
{
    // TODO : Read inputs
    var companyCode = "TESTAPI";
    var startDate = DateTime.Parse("2019-08-05");
    var endDate = DateTime.Parse("2019-09-15");

    var timesheetProcessor = host.Services.GetService<ITimesheetProcessor>();
    await timesheetProcessor.Process(companyCode, startDate, endDate);
    Console.ReadKey();
}


// Bulder and IOC
static IHostBuilder CreateHostBuilder(string[] args)
{
    var hostBuilder = Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((context, builder) =>
        {
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json", optional: false);
        })
        .ConfigureServices((context, services) =>
        {
            var configuration = context.Configuration;
            services.Configure<AppSettings>(configuration);

            // applications services
            services.AddCustomServices(configuration);
        });
    
    return hostBuilder;
}