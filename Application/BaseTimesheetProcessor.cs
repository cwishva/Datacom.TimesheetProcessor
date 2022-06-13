using CsvHelper;

namespace Application
{
    public abstract class BaseTimesheetProcessor
    {
        protected readonly IDatacomService _datacomService;

        public BaseTimesheetProcessor(IDatacomService datacomService)
        {
            this._datacomService = datacomService;
        }

        protected virtual async Task<Company> GetCompany(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new TimesheetProcessorException($"Invalid company code:{code}");
            }

            var companies = await this._datacomService.GetCompanies();
            var company = companies.FirstOrDefault(x => x.Code == code);
            if (company == null)
            {
                throw new TimesheetProcessorException($"Company code:{code} not available");
            }

            return company;
        }

        protected virtual async Task<List<string>> GetPayGroupIds(string companyId)
        {
            if (string.IsNullOrEmpty(companyId))
            {
                throw new TimesheetProcessorException($"Invalid company id:{companyId}");
            }

            var playgroups = await this._datacomService.GetPaygroups(companyId);
            return playgroups.Select(s => s.Id).ToList();
        }

        protected virtual async Task<List<Payrun>> GetPayruns()
        {
            var statuses = new List<string> 
            {
                "in progress",
                "finalised",
            };
            var playruns = await this._datacomService.GetPayruns(string.Join(',', statuses));
            return playruns;
        }

        protected virtual async Task<List<Timesheet>> GetTimesheets(int payrunId)
        {
            if (payrunId <=0)
            {
                throw new TimesheetProcessorException($"Invalid payrun id:{payrunId}");
            }

            // Get Timesheet with type=“API” by payrunId
            var timesheets = await this._datacomService.GetTimesheets(payrunId);
            return timesheets;
        }

        protected virtual void ReportData(List<TimeSheetModel> timeSheetModels)
        {
            // Data
            var all = timeSheetModels
                .GroupBy(s => new { s.PayRunId, s.EmployeeId, s.StartTime })
                .Select(x => new TimeSheetModel
                {
                   PayRunId = x.Key.PayRunId,
                   EmployeeId = x.Key.EmployeeId,
                   StartTime = x.Key.StartTime,
                   Sum = x.Sum(y => y.Sum) 
                })
                .ToList();

            // Report Data
            foreach (var item in all)
            {
                Console.WriteLine(item);
            }
        }
    }
}
