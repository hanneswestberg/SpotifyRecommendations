using MediatR;
using SpotifyRecommendations.Application.Common.ValueObjects;
using SpotifyRecommendations.Application.Spotify.Interfaces;
using SpotifyRecommendations.Application.Spotify.Models;

namespace SpotifyRecommendations.Application.Spotify.Commands.AddTrackToUserPreferenceCommand;

public class AddTrackToUserPreferenceCommand : IRequest<Result>
{
    public Track Track { get; set; } = null!;
}

public class AddTrackToUserPreferenceCommandHandler : IRequestHandler<AddTrackToUserPreferenceCommand, Result>
{
    private readonly IUserPreferenceService _userPreferenceService;

    public AddTrackToUserPreferenceCommandHandler(IUserPreferenceService userPreferenceService)
    {
        _userPreferenceService = userPreferenceService;
    }

    public Task<Result> Handle(AddTrackToUserPreferenceCommand request, CancellationToken cancellationToken)
    {
        _userPreferenceService.AddTrack(request.Track);
        return Task.FromResult(Result.Success);
    }
}