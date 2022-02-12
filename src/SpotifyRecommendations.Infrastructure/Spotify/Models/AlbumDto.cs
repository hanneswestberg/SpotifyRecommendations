using System.Text.Json.Serialization;

namespace SpotifyRecommendations.Infrastructure.Spotify.Models;

public class AlbumDto
{
    [JsonPropertyName("id")] 
    public string? Id { get; set; } = null;
        
    [JsonPropertyName("name")] 
    public string? Name { get; set; } = null;
        
    [JsonPropertyName("images")]
    public IEnumerable<ImageDto?> Images { get; set; } = Array.Empty<ImageDto>();
        
    public class ImageDto
    {
        [JsonPropertyName("url")]
        public string? Url { get; set; }
        
        [JsonPropertyName("height")]
        public int? Height { get; set; }
        
        [JsonPropertyName("width")]
        public int? Width { get; set; }
    }
}