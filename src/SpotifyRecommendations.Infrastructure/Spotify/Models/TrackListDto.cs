using System.Text.Json.Serialization;

namespace SpotifyRecommendations.Infrastructure.Spotify.Models;

public class TrackListDto
{
    [JsonPropertyName("items")]
    public IEnumerable<TrackDto?> Items { get; set; } = Array.Empty<TrackDto>();
        
    [JsonPropertyName("total")]
    public int Total { get; set; }
}