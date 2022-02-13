using MediatR;
using SpotifyRecommendations.Application.Spotify.Interfaces;
using SpotifyRecommendations.Application.Spotify.Models;

namespace SpotifyRecommendations.Application.Spotify.Queries.GetUserPreferenceQuery;

public class GetUserPreferenceQuery : IRequest<IEnumerable<Track>>
{
}

public class GetUserPreferenceQueryHandler : IRequestHandler<GetUserPreferenceQuery, IEnumerable<Track>>
{
    private readonly IUserPreferenceService _userPreferenceService;

    public GetUserPreferenceQueryHandler(IUserPreferenceService userPreferenceService)
    {
        _userPreferenceService = userPreferenceService;
    }

    public Task<IEnumerable<Track>> Handle(GetUserPreferenceQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_userPreferenceService.GetTracks());
    }
}