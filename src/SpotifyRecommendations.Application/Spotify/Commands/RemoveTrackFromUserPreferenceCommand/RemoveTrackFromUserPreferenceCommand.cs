using MediatR;
using SpotifyRecommendations.Application.Common.ValueObjects;
using SpotifyRecommendations.Application.Spotify.Interfaces;
using SpotifyRecommendations.Application.Spotify.Models;

namespace SpotifyRecommendations.Application.Spotify.Commands.RemoveTrackFromUserPreferenceCommand;

public class RemoveTrackFromUserPreferenceCommand : IRequest<Result>
{
    public Track Track { get; set; } = null!;
}

public class RemoveTrackFromUserPreferenceCommandHandler : IRequestHandler<RemoveTrackFromUserPreferenceCommand, Result>
{
    private readonly IUserPreferenceService _userPreferenceService;

    public RemoveTrackFromUserPreferenceCommandHandler(IUserPreferenceService userPreferenceService)
    {
        _userPreferenceService = userPreferenceService;
    }

    public Task<Result> Handle(RemoveTrackFromUserPreferenceCommand request, CancellationToken cancellationToken)
    {
        _userPreferenceService.RemoveTrack(request.Track);
        return Task.FromResult(Result.Success);
    }
}