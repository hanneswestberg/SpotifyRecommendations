namespace SpotifyRecommendations.Application.Common.Interfaces;

public interface IRepository
{
    Task<T?> Get<T>(string url, CancellationToken cancellationToken = default);
}