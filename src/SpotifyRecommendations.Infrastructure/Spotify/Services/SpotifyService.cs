using Microsoft.Extensions.Options;
using SpotifyRecommendations.Application.Spotify.Interfaces;
using SpotifyRecommendations.Application.Spotify.Models;
using SpotifyRecommendations.Application.Spotify.Options;
using SpotifyRecommendations.Application.Spotify.Queries.GetGenresQuery;
using SpotifyRecommendations.Application.Spotify.Queries.GetRecommendationsQuery;
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

    public async Task<GenresResponse> GetGenres(CancellationToken cancellationToken = default)
    {
        var responseDto = await _spotifyRepository.Get<GenresResponseDto>($"{_options.Value.Endpoints!.GetGenres}", cancellationToken);

        var response = new GenresResponse();
        if (responseDto?.Genres is not { })
            return response;

        response.Genres = responseDto.Genres.ToList();

        return response;
    }

    public async Task<RecommendationsResponse> GetRecommendations(GetRecommendationsQuery getRecommendationsQuery, CancellationToken cancellationToken = default)
    {
        var recommendationsQueryString = QueryBuilder.BuildRecommendationsQuery(getRecommendationsQuery);
        var responseDto = await _spotifyRepository.Get<RecommendationsResponseDto>(
            $"{_options.Value.Endpoints!.GetRecommendations}?market=sv{recommendationsQueryString}", cancellationToken);

        var response = new RecommendationsResponse();
        if (responseDto?.Tracks is not { })
            return response;

        response.TotalTracks = responseDto.Seeds.Sum(x => x.InitialPoolSize);
        
        // Todo: Move mapping of objects to a mapper
        foreach (var track in responseDto.Tracks)
        {
            response.Tracks.Add(new Track
            {
                Name = track.Name,
                Id = track.Id,
                Album = track.Album!.Name,
                Artist = string.Join(", ", track.Artists.Select(artist => artist!.Name)),
                ImageUrl = track.Album?.Images?
                    .OrderByDescending(x => x!.Height)
                    .FirstOrDefault()?
                    .Url ?? null
            });
        }
        
        return response;
    }

    public async Task<SearchResult> Search(SearchQuery searchQuery, CancellationToken cancellationToken = default)
    {
        var searchQueryString = QueryBuilder.BuildSearchQuery(searchQuery);
        
        // TODO: Move input validation to a middleware
        if (string.IsNullOrWhiteSpace(searchQueryString))
            return new SearchResult();
        
        var responseDto = await _spotifyRepository
            .Get<SearchResponseDto>($"{_options.Value.Endpoints!.SearchRequest}?type=track&market=SV&include_external=audio&q={searchQueryString}&limit={searchQuery.Limit}&offset={searchQuery.Offset}", 
                cancellationToken);

        var response = new SearchResult();
        if (responseDto?.Tracks?.Items is null)
            return response;
        
        // Todo: Move mapping of objects to a mapper
        foreach (var track in responseDto.Tracks.Items)
        {
            response.Tracks.Add(new Track
            {
                Name = track!.Name,
                Id = track.Id,
                Album = track.Album!.Name,
                Artist = string.Join(", ", track.Artists.Select(artist => artist!.Name)),
                ImageUrl = track.Album?.Images?
                    .OrderByDescending(x => x!.Height)
                    .FirstOrDefault()?
                    .Url ?? null
            });
        }

        return response;
    }
}