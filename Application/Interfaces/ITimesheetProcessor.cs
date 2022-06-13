
namespace Application.Interfaces
{
    public interface ITimesheetProcessor
    {
        Task Process(string CompanyCode, DateTime start, DateTime end);
    }
}
