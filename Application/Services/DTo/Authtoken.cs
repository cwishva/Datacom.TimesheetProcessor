
namespace Application.Services.DTo
{
    public record Authtoken
    {
        public string access_token { get; init; }
        public int expires_in { get; init; }
        public string token_type { get; init; }
        public string scope { get; init; }
    }
}
