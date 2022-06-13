
namespace Application.Services
{
    public interface IDatacomService
    {
        [Get(API.Company.Get_Companies)]
        [Header("Authorization", "Bearer")]
        Task<List<Company>> GetCompanies();

        [Get(API.Pay.Get_Paygroups)]
        [Header("Authorization", "Bearer")]
        Task<List<Paygroups>> GetPaygroups([Query("companyid")] string companyId);

        [Get(API.Pay.Get_Payruns)]
        [Header("Authorization", "Bearer")]
        Task<List<Payrun>> GetPayruns([Query] string statuses);

        [Get(API.Timesheet.Get_Timesheets)]
        [Header("Authorization", "Bearer")]
        Task<List<Timesheet>> GetTimesheets([Query("payrunid")] int payrunId);
    }
}
