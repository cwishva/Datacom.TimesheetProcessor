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
    // Note : Support Docker compose Env and docker default values due to console read issues
    Console.WriteLine("Enter Company Code: eg:(TESTAPI)");
    var ENV_COMPANY = Environment.GetEnvironmentVariable("COMPANY");
    var companyCode = ReadLine(string.IsNullOrEmpty(ENV_COMPANY) ? "TESTAPI" : ENV_COMPANY);

    Console.WriteLine("Enter Start Date: eg:(2019-08-05)");
    var ENV_STARTDATE = Environment.GetEnvironmentVariable("STARTDATE");
    string startDateInput = ReadLine(string.IsNullOrEmpty(ENV_STARTDATE) ? "2019-08-05" : ENV_STARTDATE);
    var startDate = TextExtension.ParseDate(startDateInput);

    Console.WriteLine("Enter End Date: eg:(2019-09-15)");
    var ENV_ENDDATE = Environment.GetEnvironmentVariable("ENDDATE");
    string endDateInput = ReadLine(string.IsNullOrEmpty(ENV_ENDDATE) ? "2019-09-15" : ENV_ENDDATE);
    var endDate = TextExtension.ParseDate(endDateInput);

    // Test Value
    //companyCode = "TESTAPI";
    //startDate = TextExtension.ParseDate("2019-08-05");
    //endDate = TextExtension.ParseDate("2019-09-15");

    var timesheetProcessor = host.Services.GetService<ITimesheetProcessor>();
    var status = await timesheetProcessor.Process(companyCode, startDate, endDate);
    Console.WriteLine($"Console app status : {status}");
    ReadLine(string.Empty);
}


// Bulder and IOC
static IHostBuilder CreateHostBuilder(string[] args)
{
    var hostBuilder = Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((context, builder) =>
        {
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json", optional: false);
            builder.AddEnvironmentVariables();
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

static string ReadLine(string defaultValue)
{
    var input = defaultValue;
    // Docker console conflit check
    if (!Console.IsInputRedirected)
    {
        input = Console.In.ReadLine();
    }
    
    return input;
}