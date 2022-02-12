using Microsoft.Extensions.Options;
using SpotifyRecommendations.Application.Spotify.Interfaces;
using SpotifyRecommendations.Application.Spotify.Options;
using SpotifyRecommendations.Application.Spotify.Queries.SearchQuery;
using SpotifyRecommendations.Infrastructure.Spotify.Helpers;
using SpotifyRecommendations.Infrastructure.Spotify.Models;

namespace SpotifyRecommendations.Infrastructure.Spotify.Services;

public class SpotifyService : ISpotifyService
{
    private readonly ISpotifyRepository _spotifyRepository;
    private readonly IOptions<SpotifyOptions> _options;

    public SpotifyService(ISpotifyRepository spotifyRepository, IOptions<SpotifyOptions> options)
    {
        _spotifyRepository = spotifyRepository;
        _options = options;
    }

    public async Task<SearchResult> Search(SearchQuery searchQuery, CancellationToken cancellationToken = default)
    {
        var searchQueryString = SearchQueryBuilder.Build(searchQuery);
        var response = await _spotifyRepository
            .Get<SearchResultDto>($"{_options.Value.Endpoints!.SearchRequest}?type=track&market=SV&include_external=audio&q={searchQueryString}&limit={searchQuery.Limit}&offset={searchQuery.Offset}", 
                cancellationToken);
        
        

        return new SearchResult();
    }
}