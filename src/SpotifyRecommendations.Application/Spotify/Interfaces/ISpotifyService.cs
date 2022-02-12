using SpotifyRecommendations.Application.Spotify.Queries.GetGenresQuery;
using SpotifyRecommendations.Application.Spotify.Queries.SearchQuery;

namespace SpotifyRecommendations.Application.Spotify.Interfaces;

public interface ISpotifyService
{
    Task<GenresResponse> GetGenres(CancellationToken cancellationToken = default);
    Task<SearchResult> Search(SearchQuery searchQuery, CancellationToken cancellationToken = default);
}