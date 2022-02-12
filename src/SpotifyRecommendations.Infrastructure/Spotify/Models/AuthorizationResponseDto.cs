using System.Text.Json.Serialization;

namespace SpotifyRecommendations.Infrastructure.Spotify.Models;

public class AuthorizationResponseDto
{
    [JsonPropertyName("access_token")] 
    public string AccessToken { get; set; } = null!;
}