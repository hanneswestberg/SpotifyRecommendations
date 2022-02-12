namespace SpotifyRecommendations.Application.Common.Interfaces;

public interface IHttpClientFactory
{
    HttpClient GetClientByUri(Uri uri);
}