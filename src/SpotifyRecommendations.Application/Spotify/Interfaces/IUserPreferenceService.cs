using SpotifyRecommendations.Application.Spotify.Models;

namespace SpotifyRecommendations.Application.Spotify.Interfaces;

public interface IUserPreferenceService
{
    IEnumerable<Track> GetTracks();
    void AddTrack(Track track);
    void RemoveTrack(Track track);
}