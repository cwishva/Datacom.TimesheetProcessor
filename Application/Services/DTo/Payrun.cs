
using System.ComponentModel.DataAnnotations;

namespace Application.Services.DTo
{
    public record Payrun
    {
        public int Id { get; init; }
        public string PayGroupId { get; init; }
        public string Status { get; init; }
        public PayPeriod PayPeriod { get; init; }
        public DateTime? FinalisationDate { get; init; }
    }

    public record PayPeriod
    {
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
    }
}
