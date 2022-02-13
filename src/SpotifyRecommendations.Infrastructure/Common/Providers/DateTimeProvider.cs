using SpotifyRecommendations.Application.Common.Interfaces;

namespace SpotifyRecommendations.Infrastructure.Common.Providers;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now()
    {
        return DateTime.Now;
    }
}