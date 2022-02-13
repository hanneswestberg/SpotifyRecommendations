using Microsoft.Extensions.DependencyInjection;
using SpotifyRecommendations.Application.Common.Interfaces;
using SpotifyRecommendations.Application.Spotify.Interfaces;
using SpotifyRecommendations.Infrastructure.Common.Factories;
using SpotifyRecommendations.Infrastructure.Common.Providers;
using SpotifyRecommendations.Infrastructure.Spotify.Repositories;
using SpotifyRecommendations.Infrastructure.Spotify.Services;

namespace SpotifyRecommendations.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection  AddInfrastructure(this IServiceCollection  services)
    {
        services.AddSingleton<IHttpClientFactory, HttpClientFactory>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddSingleton<ISpotifyRepository, SpotifyRepository>();
        services.AddSingleton<IUserPreferenceService, UserPreferenceService>();
        
        services.AddScoped<ISpotifyService, SpotifyService>();
        
        return services;
    }
}