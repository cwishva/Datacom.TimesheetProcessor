namespace Infrastructure.Filters
{
    public class GlobalExceptionFilter
    {
        public static void TimesheetProcessorExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine(e.ExceptionObject.ToString());
            Console.WriteLine("Press Enter to Exit");
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}