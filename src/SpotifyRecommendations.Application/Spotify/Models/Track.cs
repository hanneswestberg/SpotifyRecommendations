namespace SpotifyRecommendations.Application.Spotify.Models;

public class Track
{
    public string? Name { get; set; }
    public string? Id { get; set; }
    public Album? Album { get; set; }
    public List<Artist> Artists { get; set; } = new();
}