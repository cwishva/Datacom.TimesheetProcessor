
namespace Application.Services.DTo
{
    public record Paygroups
    {
        public string Id { get; init; }
        public string CompanyId { get; init; }
        public string Code { get; init; }
        public string Name { get; init; }
    }
}
