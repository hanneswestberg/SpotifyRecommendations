using SpotifyRecommendations.Application.Spotify.Models;

namespace SpotifyRecommendations.Models;

public class GetRecommendationsViewModel
{
    public List<Track> RecommendedTracks { get; set; } = new();
}