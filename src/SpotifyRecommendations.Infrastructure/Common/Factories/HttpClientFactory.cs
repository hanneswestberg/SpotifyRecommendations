using System.Collections.Concurrent;
using SpotifyRecommendations.Application.Common.Interfaces;

namespace SpotifyRecommendations.Infrastructure.Common.Factories;

public class HttpClientFactory : IHttpClientFactory
{
    private static readonly ConcurrentDictionary<string, HttpClient> HttpClientCache = new();

    public HttpClient GetClientByUri(Uri uri)
    {
        var key = $"{uri.Scheme}://{uri.DnsSafeHost}:{uri.Port}";

        return HttpClientCache.GetOrAdd(key, k =>
        {
            var client = new HttpClient
            {
                BaseAddress = uri
            };
            
            return client;
        });
    }
}