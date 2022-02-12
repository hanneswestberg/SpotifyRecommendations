using System.Text.Json.Serialization;

namespace SpotifyRecommendations.Infrastructure.Spotify.Models;

public class TrackDto
{
    [JsonPropertyName("id")] 
    public string? Id { get; set; } = null;
        
    [JsonPropertyName("name")] 
    public string? Name { get; set; } = null;
        
    [JsonPropertyName("album")] 
    public AlbumDto? Album { get; set; } = null;
        
    [JsonPropertyName("artists")] 
    public IEnumerable<ArtistDto?> Artists { get; set; } = Array.Empty<ArtistDto>();
}