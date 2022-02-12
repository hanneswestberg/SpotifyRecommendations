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
    
    public SpotifyRepository(IHttpClientFactory httpClientFactory, IOptions<SpotifyOptions> options)
    {
        _httpClientFactory = httpClientFactory;
        _options = options;
    }
    
    private async Task<AuthorizationResponseDto> Auth(CancellationToken cancellationToken = default)
    {
        var httpClient = _httpClientFactory.GetClientByUri(new Uri(_options.Value.AuthBaseAddress!));

        var authKeyText = $"{_options.Value.ClientId}:{_options.Value.ClientSecret}";
        var authKeyBytes = Encoding.ASCII.GetBytes(authKeyText);
        var authKeyEncoded = Convert.ToBase64String(authKeyBytes);
        
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authKeyEncoded);

        var response = await httpClient.PostAsync(_options.Value.Endpoints!.RequestAuthorization,
            new FormUrlEncodedContent(new [] {
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            }), cancellationToken);

        response.EnsureSuccessStatusCode();
        
        return (await response.Content.ReadFromJsonAsync<AuthorizationResponseDto>(cancellationToken: cancellationToken))!;
    }
    
    public async Task<T?> Get<T>(string url, CancellationToken cancellationToken = default)
    {
        // Todo: Make a smarter authentication. No need to authenticate on each request
        var bearer = await Auth(cancellationToken);

        var httpClient = _httpClientFactory.GetClientByUri(new Uri(_options.Value.BaseAddress!));
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer.AccessToken);
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));

        var response = await httpClient.GetAsync(url, cancellationToken);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<T>(cancellationToken: cancellationToken);
    }
}