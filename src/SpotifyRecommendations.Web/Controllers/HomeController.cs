using System.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SpotifyRecommendations.Application.Spotify.Interfaces;
using SpotifyRecommendations.Application.Spotify.Models;
using SpotifyRecommendations.Application.Spotify.Queries.GetGenresQuery;
using SpotifyRecommendations.Application.Spotify.Queries.GetRecommendationsQuery;
using SpotifyRecommendations.Application.Spotify.Queries.SearchQuery;
using SpotifyRecommendations.Infrastructure.Spotify.Helpers;
using SpotifyRecommendations.Models;

namespace SpotifyRecommendations.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMediator _mediator;
    private readonly IUserPreferenceService _userPreferenceService;

    public HomeController(ILogger<HomeController> logger, IMediator mediator, IUserPreferenceService userPreferenceService)
    {
        _logger = logger;
        _mediator = mediator;
        _userPreferenceService = userPreferenceService;
    }

    public async Task<IActionResult> Index()
    {
        var genreResponse = await _mediator.Send(new GetGenresQuery());
        
        var viewModel = new SearchViewModel
        {
            Genres = genreResponse.Genres
        };
        
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Search([FromForm] SearchQuery query)
    {
        var genreResponse = await _mediator.Send(new GetGenresQuery());
        var searchResponse = await _mediator.Send(query);

        var viewModel = new SearchViewModel
        {
            Genres = genreResponse.Genres,
            Tracks = searchResponse.Tracks,
            SearchQuery = query
        };
        
        return View("~/Views/Home/Index.cshtml", viewModel);
    }

    [HttpPost]
    public IActionResult AddTrackToPreferenceList([FromForm] Track track)
    {
        _userPreferenceService.AddTrack(track);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult RemoveTrackFromPreferenceList([FromForm] Track track)
    {
        _userPreferenceService.RemoveTrack(track);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> GetRecommendations()
    {
        var userPreferenceTracks = _userPreferenceService.GetTracks();
        var response = await _mediator.Send(new GetRecommendationsQuery
        {
            TrackIds = userPreferenceTracks.Select(x => x.Id)
        });

        var viewModel = new GetRecommendationsViewModel
        {
            RecommendedTracks = response.Tracks.ToList(),
        };
        
        return View(viewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}