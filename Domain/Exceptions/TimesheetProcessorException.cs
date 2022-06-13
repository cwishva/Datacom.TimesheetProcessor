
namespace Domain.Exceptions
{
    public class TimesheetProcessorException : Exception
    {
        public TimesheetProcessorException() { }

        public TimesheetProcessorException(string message)
            : base(message) { }

        public TimesheetProcessorException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
