namespace SpotifyRecommendations.Application.Spotify.Models;

public class Track
{
    public string? Name { get; set; }
    public string? Id { get; set; }
    public string? Album { get; set; }
    public string? Artist { get; set; }
    public string? ImageUrl { get; set; }
    public string? ExternalSpotifyUrl { get; set; }
}