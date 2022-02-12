using SpotifyRecommendations.Application.Spotify.Models;
using SpotifyRecommendations.Application.Spotify.Queries.SearchQuery;

namespace SpotifyRecommendations.Models;

public class GetRecommendationsViewModel
{
    public List<Track> RecommendedTracks { get; set; }
    public SearchQuery SearchQuery { get; set; }
}