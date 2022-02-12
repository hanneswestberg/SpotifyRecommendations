using System.Text.Json.Serialization;

namespace SpotifyRecommendations.Infrastructure.Spotify.Models;

public class SearchResultDto
{
    [JsonPropertyName("tracks")]
    public TrackListDto? Tracks { get; init; }
    
    public class TrackListDto
    {
        [JsonPropertyName("items")]
        public TrackListDto? Items { get; init; }
    }

    public class TrackDto
    {
        [JsonPropertyName("id")] 
        public string? Id { get; init; } = null;
        
        [JsonPropertyName("name")] 
        public string? Name { get; init; } = null;
        
        [JsonPropertyName("album")] 
        public AlbumDto? Album { get; init; } = null;
        
        [JsonPropertyName("artists")] 
        public IEnumerable<ArtistDto?> Artists { get; init; } = Array.Empty<ArtistDto>();
    }

    public class ArtistDto
    {
        [JsonPropertyName("id")] 
        public string? Id { get; init; } = null;
        
        [JsonPropertyName("name")] 
        public string? Name { get; init; } = null;
    }

    public class AlbumDto
    {
        [JsonPropertyName("id")] 
        public string? Id { get; init; } = null;
        
        [JsonPropertyName("name")] 
        public string? Name { get; init; } = null;
    }
}

