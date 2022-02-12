using Microsoft.Extensions.Options;
using SpotifyRecommendations.Application.Common.Interfaces;
using SpotifyRecommendations.Application.Spotify.Interfaces;
using SpotifyRecommendations.Application.Spotify.Options;

namespace SpotifyRecommendations.Infrastructure.Spotify.Repositories;

public class SpotifyRepository : ISpotifyRepository
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOptions<SpotifyOptions> _options;
    
    public SpotifyRepository(IHttpClientFactory httpClientFactory, IOptions<SpotifyOptions> options)
    {
        _httpClientFactory = httpClientFactory;
        _options = options;
    }
    
    public Task<T?> Get<T>(string url, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}