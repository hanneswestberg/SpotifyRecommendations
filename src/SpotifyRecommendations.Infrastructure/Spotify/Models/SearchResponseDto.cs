using System.Text.Json.Serialization;

namespace SpotifyRecommendations.Infrastructure.Spotify.Models;

public class SearchResponseDto
{
    [JsonPropertyName("tracks")]
    public TrackListDto? Tracks { get; set; }
    
    public class TrackListDto
    {
        [JsonPropertyName("items")]
        public IEnumerable<TrackDto?> Items { get; set; } = Array.Empty<TrackDto>();
        
        [JsonPropertyName("total")]
        public int Total { get; set; }
    }

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

    public class ArtistDto
    {
        [JsonPropertyName("id")] 
        public string? Id { get; set; } = null;
        
        [JsonPropertyName("name")] 
        public string? Name { get; set; } = null;
    }

    public class AlbumDto
    {
        [JsonPropertyName("id")] 
        public string? Id { get; set; } = null;
        
        [JsonPropertyName("name")] 
        public string? Name { get; set; } = null;
    }
}

