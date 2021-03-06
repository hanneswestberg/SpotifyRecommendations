using System.Text.Json.Serialization;

namespace SpotifyRecommendations.Infrastructure.Spotify.Models;

public class SearchResponseDto
{
    [JsonPropertyName("tracks")]
    public TrackListDto? Tracks { get; set; }
}

