using System.Text.Json.Serialization;

namespace SpotifyRecommendations.Infrastructure.Spotify.Models;

public class ArtistDto
{
    [JsonPropertyName("id")] 
    public string? Id { get; set; } = null;
        
    [JsonPropertyName("name")] 
    public string? Name { get; set; } = null;
}