
namespace Application.Services.DTo
{
    public record Timesheet
    {
        public string Id { get; init; }
        public string PayrunId { get; init; }
        public string Name { get; init; }
        public string Status { get; init; }
        public string Type { get; init; }
        public List<TimesheetValues> Values { get; init; }
    }

    public record TimesheetValues
    {
        public string Id { get; init; }
        public string EmployeeId { get; init; }
        public DateTime? StartDate { get; init; }
        public DateTime? EndDate { get; init; }
        public decimal Value { get; init; }
    }
}
