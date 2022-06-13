
namespace Domain.Common
{
    public class AppSettings
    {
        public API API { get; set; }
    }

    public class API
    {
        public string AuthUrl { get; set; }
        public string ClientId { get; set; }
        public string Secret { get; set; }
        public string BaseUrl { get; set; }
    }
}
