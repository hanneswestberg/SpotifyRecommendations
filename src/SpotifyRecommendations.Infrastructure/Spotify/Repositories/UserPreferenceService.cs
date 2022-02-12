using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SpotifyRecommendations.Application.Spotify.Interfaces;
using SpotifyRecommendations.Application.Spotify.Models;

namespace SpotifyRecommendations.Infrastructure.Spotify.Repositories;

public class UserPreferenceService : IUserPreferenceService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string TracksSessionKey = "TracksSessionKey";

    public UserPreferenceService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public IEnumerable<Track> GetTracks()
    {
        var tracksSessionString = _httpContextAccessor.HttpContext!.Session.GetString(TracksSessionKey);

        if (string.IsNullOrWhiteSpace(tracksSessionString))
            return new List<Track>();
        
        var tracks = JsonConvert.DeserializeObject<IEnumerable<Track>>(tracksSessionString);
        
        return tracks ?? new List<Track>();
    }

    public void AddTrack(Track track)
    {
        var tracks = GetTracks().ToList();
        
        if (tracks.Contains(track))
            return;
        
        tracks.Add(track);
        
        var tracksSessionString = JsonConvert.SerializeObject(tracks);
        
        _httpContextAccessor.HttpContext!.Session.SetString(TracksSessionKey, tracksSessionString);
    }

    public void RemoveTrack(Track track)
    {
        var tracks = GetTracks().ToList();
        
        if (tracks.All(x => x.Id != track.Id))
            return;
        
        tracks.RemoveAll(x => x.Id == track.Id);
        
        var tracksSessionString = JsonConvert.SerializeObject(tracks);
        
        _httpContextAccessor.HttpContext!.Session.SetString(TracksSessionKey, tracksSessionString);
    }
}