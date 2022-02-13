using System.Text.Json.Serialization;

namespace SpotifyRecommendations.Infrastructure.Spotify.Models;

public class RecommendationsResponseDto
{
    [JsonPropertyName("tracks")]
    public IEnumerable<TrackDto> Tracks { get; set; } = ArraySegment<TrackDto>.Empty;

    [JsonPropertyName("seeds")]
    public IEnumerable<SeedDto> Seeds { get; set; } = ArraySegment<SeedDto>.Empty;
    
    public class SeedDto
    {
        [JsonPropertyName("initialPoolSize")]
        public int InitialPoolSize { get; set; }
        
        [JsonPropertyName("afterFilteringSize")]
        public int AfterFilteringSize { get; set; }
    }
}
