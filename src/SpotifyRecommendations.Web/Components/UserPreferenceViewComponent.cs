using Microsoft.AspNetCore.Mvc;
using SpotifyRecommendations.Application.Spotify.Interfaces;

namespace SpotifyRecommendations.Web.Components;

public class UserPreferenceViewComponent : ViewComponent
{
    private readonly IUserPreferenceService _userPreferenceService;

    public UserPreferenceViewComponent(IUserPreferenceService userPreferenceService)
    {
        _userPreferenceService = userPreferenceService;
    }

    public Task<IViewComponentResult> InvokeAsync()
    {
        var userPreferenceTracks = _userPreferenceService.GetTracks();

        return Task.FromResult<IViewComponentResult>(View("~/Views/Components/UserPreference/UserPreference.cshtml", userPreferenceTracks));
    }
}