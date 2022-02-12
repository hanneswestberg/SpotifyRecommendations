using MediatR;
using SpotifyRecommendations.Application.Spotify.Interfaces;

namespace SpotifyRecommendations.Application.Spotify.Queries.SearchQuery;

public class SearchQuery : IRequest<SearchResult>
{
    public string QueryString { get; set; } = null!;
    public string Artist { get; init; } = null!;
    public string Album { get; init; } = null!;
    public string YearStart { get; init; } = null!;
    public string YearEnd { get; init; } = null!;
    public string Genre { get; init; } = null!;

    public bool TagNew { get; init; } = false;
    public bool TagHipster { get; init; } = false;

    public int Limit { get; init; } = 9;
    public int Offset { get; init; } = 0;
}

public class SearchQueryHandler : IRequestHandler<SearchQuery, SearchResult>
{
    private readonly ISpotifyService _spotifyService;

    public SearchQueryHandler(ISpotifyService spotifyService)
    {
        _spotifyService = spotifyService;
    }

    public async Task<SearchResult> Handle(SearchQuery request, CancellationToken cancellationToken)
    {
        return await _spotifyService.Search(request, cancellationToken);
    }
}