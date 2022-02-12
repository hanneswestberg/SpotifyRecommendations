using SpotifyRecommendations.Application.Spotify.Models;
using SpotifyRecommendations.Application.Spotify.Queries.SearchQuery;

namespace SpotifyRecommendations.Models;

public class SearchViewModel
{
    public List<string> Genres { get; set; } = new();
    public SearchQuery SearchQuery { get; set; } = null!;
    public List<Track> Tracks { get; set; } = new();
}