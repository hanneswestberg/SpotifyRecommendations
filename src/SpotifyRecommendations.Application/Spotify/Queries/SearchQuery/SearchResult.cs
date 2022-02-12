using SpotifyRecommendations.Application.Spotify.Models;

namespace SpotifyRecommendations.Application.Spotify.Queries.SearchQuery;

public class SearchResult
{
    public List<Track> Tracks { get; set; } = new();
    public int TotalHits { get; set; }
}