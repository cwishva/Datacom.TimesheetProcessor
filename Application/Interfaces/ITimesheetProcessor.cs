
namespace Application.Interfaces
{
    public interface ITimesheetProcessor
    {
        Task<bool> Process(string CompanyCode, DateTime start, DateTime end);
    }
}
