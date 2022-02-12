using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace SpotifyRecommendations.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
    }
}