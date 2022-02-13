using System.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SpotifyRecommendations.Application.Spotify.Commands.AddTrackToUserPreferenceCommand;
using SpotifyRecommendations.Application.Spotify.Commands.RemoveTrackFromUserPreferenceCommand;
using SpotifyRecommendations.Application.Spotify.Models;
using SpotifyRecommendations.Application.Spotify.Queries.GetGenresQuery;
using SpotifyRecommendations.Application.Spotify.Queries.GetRecommendationsQuery;
using SpotifyRecommendations.Application.Spotify.Queries.GetUserPreferenceQuery;
using SpotifyRecommendations.Application.Spotify.Queries.SearchQuery;
using SpotifyRecommendations.Models;

namespace SpotifyRecommendations.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMediator _mediator;

    public HomeController(ILogger<HomeController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
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
    public async Task<IActionResult> AddTrackToPreferenceList([FromForm] AddTrackToUserPreferenceCommand command)
    {
        _ = await _mediator.Send(command);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> RemoveTrackFromPreferenceList([FromForm] RemoveTrackFromUserPreferenceCommand command)
    {
        _ = await _mediator.Send(command);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> GetRecommendations()
    {
        var userPreferenceTracks = await _mediator.Send(new GetUserPreferenceQuery());
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