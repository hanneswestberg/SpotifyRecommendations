using SpotifyRecommendations.Application.Spotify.Models;

namespace SpotifyRecommendations.Application.Spotify.Queries.GetRecommendationsQuery;

public class RecommendationsResponse
{
    public List<Track> Tracks { get; set; } = new();
}