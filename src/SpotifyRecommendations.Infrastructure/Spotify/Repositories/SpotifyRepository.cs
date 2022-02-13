using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using Microsoft.Extensions.Options;
using SpotifyRecommendations.Application.Common.Interfaces;
using SpotifyRecommendations.Application.Spotify.Interfaces;
using SpotifyRecommendations.Application.Spotify.Options;
using SpotifyRecommendations.Infrastructure.Spotify.Models;

namespace SpotifyRecommendations.Infrastructure.Spotify.Repositories;

public class SpotifyRepository : ISpotifyRepository
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOptions<SpotifyOptions> _options;

    private static readonly TimeSpan AccessTokenLifeTime = new(0, 45, 0);
    private DateTime _accessTokenTimeStamp = DateTime.MinValue;

    public SpotifyRepository(IHttpClientFactory httpClientFactory, IOptions<SpotifyOptions> options)
    {
        _httpClientFactory = httpClientFactory;
        _options = options;
        
        var authHttpClient = _httpClientFactory.GetClientByUri(new Uri(_options.Value.AuthBaseAddress!));
        var authKeyText = $"{_options.Value.ClientId}:{_options.Value.ClientSecret}";
        var authKeyBytes = Encoding.ASCII.GetBytes(authKeyText);
        var authKeyEncoded = Convert.ToBase64String(authKeyBytes);
        authHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authKeyEncoded);

        var requestHttpClient = _httpClientFactory.GetClientByUri(new Uri(_options.Value.BaseAddress!));
        requestHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
    }
    
    public async Task<T?> Get<T>(string url, CancellationToken cancellationToken = default)
    {
        if (!AccessTokenIsValid())
            await Auth(cancellationToken);
        
        var httpClient = _httpClientFactory.GetClientByUri(new Uri(_options.Value.BaseAddress!));
        
        var response = await httpClient.GetAsync(url, cancellationToken);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<T>(cancellationToken: cancellationToken);
    }
    
    private async Task Auth(CancellationToken cancellationToken = default)
    {
        var authHttpClient = _httpClientFactory.GetClientByUri(new Uri(_options.Value.AuthBaseAddress!));
        
        var response = await authHttpClient.PostAsync(_options.Value.Endpoints!.RequestAuthorization,
            new FormUrlEncodedContent(new [] {
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            }), cancellationToken);

        response.EnsureSuccessStatusCode();
        
        var accessToken = await response.Content.ReadFromJsonAsync<AuthorizationResponseDto>(cancellationToken: cancellationToken);
        _accessTokenTimeStamp = DateTime.Now;

        var requestHttpClient = _httpClientFactory.GetClientByUri(new Uri(_options.Value.BaseAddress!));
        
        requestHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken!.AccessToken);
    }

    private bool AccessTokenIsValid()
    {
        return DateTime.Now - _accessTokenTimeStamp < AccessTokenLifeTime;
    }
}