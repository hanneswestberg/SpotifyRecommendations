using MediatR;
using SpotifyRecommendations.Application.Spotify.Interfaces;

namespace SpotifyRecommendations.Application.Spotify.Queries.GetGenresQuery;

public class GetGenresQuery : IRequest<GenresResponse>
{
}

public class GetGenresQueryHandler : IRequestHandler<GetGenresQuery, GenresResponse>
{
    private readonly ISpotifyService _spotifyService;

    public GetGenresQueryHandler(ISpotifyService spotifyService)
    {
        _spotifyService = spotifyService;
    }

    public async Task<GenresResponse> Handle(GetGenresQuery request, CancellationToken cancellationToken)
    {
        return await _spotifyService.GetGenres(cancellationToken);
    }
}