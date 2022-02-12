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
        
        [JsonPropertyName("external_urls")]
        public ExternalUrlDto? ExternalUrls { get; set; }
    }

    public class ExternalUrlDto
    {
        [JsonPropertyName("spotify")]
        public string Spotify { get; set; }
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
}

