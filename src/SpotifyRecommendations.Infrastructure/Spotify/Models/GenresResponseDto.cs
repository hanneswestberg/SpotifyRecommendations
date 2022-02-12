using System.Text.Json.Serialization;

namespace SpotifyRecommendations.Infrastructure.Spotify.Models;

public class GenresResponseDto
{
    [JsonPropertyName("genres")]
    public IEnumerable<string> Genres { get; set; } = Array.Empty<string>();
}