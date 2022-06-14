
namespace ConsoleApp.Extensions
{
    internal static class TextExtension
    {
        internal static DateTime ParseDate(this string text)
        {
            var formats = new[] { "yyyy-MM-dd" };
            bool isValidDateTime = DateTime.TryParseExact(
                text,
                formats, 
                CultureInfo.InvariantCulture, 
                DateTimeStyles.None, 
                out DateTime result);
            if (isValidDateTime)
            {
                return result;
            }

            throw new TimesheetProcessorException("Invlaid Date Format");
        }
    }
}
