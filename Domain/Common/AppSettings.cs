
namespace Domain.Common
{
    public record AppSettings
    {
        public API API { get; set; }
    }

    public record API
    {
        public string AuthUrl { get; set; }
        public string ClientId { get; set; }
        public string Secret { get; set; }
        public string BaseUrl { get; set; }
    }
}
