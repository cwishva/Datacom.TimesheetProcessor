
namespace Infrastructure.Services.DTo
{
    public record Payrun
    {
        public string Id { get; set; }
        public string PayGroupId { get; set; }
        public string Status { get; set; }
        public PayPeriod PayPeriod { get; set; }
        public DateTime? FinalisationDate { get; set; }
    }

    public record PayPeriod
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
