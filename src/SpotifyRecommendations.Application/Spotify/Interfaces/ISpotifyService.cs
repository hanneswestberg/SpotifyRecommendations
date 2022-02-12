using SpotifyRecommendations.Application.Spotify.Queries.SearchQuery;

namespace SpotifyRecommendations.Application.Spotify.Interfaces;

public interface ISpotifyService
{
    Task<SearchResult> Search(SearchQuery searchQuery, CancellationToken cancellationToken = default);
}