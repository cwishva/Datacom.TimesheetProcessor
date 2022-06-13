using CsvHelper;

namespace Application
{
    public class CsvTimesheetProcessor
        : BaseTimesheetProcessor, ITimesheetProcessor
    {
        public CsvTimesheetProcessor(IDatacomService datacomService) 
            : base(datacomService)
        {
        }

        public async Task Process(string CompanyCode, DateTime start, DateTime end)
        {
            // get companies and payruns
            var companyTask = base.GetCompany(CompanyCode);
            var payruns = await base.GetPayruns();

            // Validate Company
            var company = await companyTask;
            var paygroupIds = await base.GetPayGroupIds(company.Id);

            // Filter by Start, end and correct company paygroup
            var validPayrunIds = payruns
                .Where(
                    x =>
                    paygroupIds.Contains(x.PayGroupId) && 
                    x.PayPeriod.StartDate >= start && 
                    x.PayPeriod.EndDate <= end)
                .ToList();

            // Process Data for CSV
            var timeSheetModels = new List<TimeSheetModel>();
            foreach (var item in validPayrunIds)
            {
                var timesheets = await base.GetTimesheets(item.Id);
                var models = timesheets
                    .SelectMany(x => x.Values)
                    .Select(x => new TimeSheetModel
                    {
                        PayRunId = item.Id,
                        EmployeeId = x.EmployeeId,
                        StartTime = item.PayPeriod.StartDate,
                        Sum = x.Value
                    }).ToList();
                timeSheetModels.AddRange(models);
            }

            // Generate CSV
            this.ReportData(timeSheetModels);
        }

        protected override void ReportData(List<TimeSheetModel> timeSheetModels)
        {
            // CSV Data
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

            // Generate CSV
            var fileName = $"TimesheetImport_{DateTime.UtcNow:yyyyMMdd}.csv";
            using (var writer = new StreamWriter(fileName))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(all);
            }
        }
    }
}
