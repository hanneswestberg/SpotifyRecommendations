using System.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SpotifyRecommendations.Application.Spotify.Queries.GetGenresQuery;
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
        var response = await _mediator.Send(new GetGenresQuery());

        var viewModel = new HomeViewModel
        {
            Genres = response.Genres
        };
        
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> GetRecommendations([FromForm] SearchQuery query)
    {
        var response = await _mediator.Send(query);

        var viewModel = new GetRecommendationsViewModel
        {
            RecommendedTracks = response.Tracks,
            SearchQuery = query
        };
        
        return View(viewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}