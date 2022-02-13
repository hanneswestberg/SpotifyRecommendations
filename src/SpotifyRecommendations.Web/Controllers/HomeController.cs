using System.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        var likedTracks = await _mediator.Send(new GetUserPreferenceQuery());

        var viewModel = new SearchViewModel
        {
            Genres = genreResponse.Genres,
            LikedTracks = likedTracks.ToList()
        };
        
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Search([FromForm] SearchQuery query)
    {
        var genreResponse = await _mediator.Send(new GetGenresQuery());
        var likedTracks = await _mediator.Send(new GetUserPreferenceQuery());
        var searchResponse = await _mediator.Send(query);

        var viewModel = new SearchViewModel
        {
            Genres = genreResponse.Genres,
            Tracks = searchResponse.Tracks,
            SearchQuery = query,
            LikedTracks = likedTracks.ToList()
        };
        
        return View("~/Views/Home/Index.cshtml", viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddTrackToPreferenceList([FromForm] AddTrackToUserPreferenceCommand command, string trackData, string searchQuery)
    {
        await _mediator.Send(command);
        
        var genreResponse = await _mediator.Send(new GetGenresQuery());
        var likedTracks = await _mediator.Send(new GetUserPreferenceQuery());

        var viewModel = new SearchViewModel
        {
            Genres = genreResponse.Genres,
            Tracks = JsonConvert.DeserializeObject<List<Track>>(trackData),
            SearchQuery = JsonConvert.DeserializeObject<SearchQuery>(searchQuery),
            LikedTracks = likedTracks.ToList()
        };
        
        return View("~/Views/Home/Index.cshtml", viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> RemoveTrackFromPreferenceList([FromForm] RemoveTrackFromUserPreferenceCommand command, string trackData, string searchQuery)
    {
        await _mediator.Send(command);
                
        var genreResponse = await _mediator.Send(new GetGenresQuery());
        var likedTracks = await _mediator.Send(new GetUserPreferenceQuery());

        var viewModel = new SearchViewModel
        {
            Genres = genreResponse.Genres,
            Tracks = JsonConvert.DeserializeObject<List<Track>>(trackData),
            SearchQuery = JsonConvert.DeserializeObject<SearchQuery>(searchQuery),
            LikedTracks = likedTracks.ToList()
        };
        
        return View("~/Views/Home/Index.cshtml", viewModel);
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
            TotalTracks = response.TotalTracks
        };
        
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> RefineRecommendations([FromForm] GetRecommendationsQuery query)
    {
        var userPreferenceTracks = await _mediator.Send(new GetUserPreferenceQuery());
        query.TrackIds = userPreferenceTracks.Select(x => x.Id);
        
        var response = await _mediator.Send(query);

        var viewModel = new GetRecommendationsViewModel
        {
            RecommendedTracks = response.Tracks.ToList(),
            TotalTracks = response.TotalTracks
        };
        
        return View("~/Views/Home/GetRecommendations.cshtml", viewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}