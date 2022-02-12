namespace SpotifyRecommendations.Application.Spotify.Options;

public class SpotifyOptions
{
    public string? ClientId { get; set; } = null;
    public string? ClientSecret { get; set; } = null;
    public string? BaseAddress { get; set; } = null;
    public string? AuthBaseAddress { get; set; } = null;
    public SpotifyEndpoints? Endpoints { get; set; } = null;
    public class SpotifyEndpoints
    {
        public string? RequestAuthorization { get; set; } = null;
        public string? SearchRequest { get; set; } = null;
        public string? GetGenres { get; set; } = null;
        public string? GetRecommendations { get; set; } = null;
    }
}