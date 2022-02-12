using System.Text.Json.Serialization;

namespace SpotifyRecommendations.Infrastructure.Spotify.Models;

public class RecommendationsResponseDto
{
    [JsonPropertyName("tracks")]
    public IEnumerable<TrackDto>? Tracks { get; set; }
}
