using MediatR;
using SpotifyRecommendations.Application.Spotify.Interfaces;

namespace SpotifyRecommendations.Application.Spotify.Queries.GetRecommendationsQuery;

public class GetRecommendationsQuery : IRequest<RecommendationsResponse>
{
    public IEnumerable<string?> TrackIds { get; set; } = Array.Empty<string>();
    public int? TargetDanceability { get; set; } = null;
    public int? TargetAcousticness { get; set; } = null;
    public int? TargetEnergy { get; set; } = null;
    public int? TargetPopularity { get; set; } = null;
}

public class GetRecommendationsQueryHandler : IRequestHandler<GetRecommendationsQuery, RecommendationsResponse>
{
    private readonly ISpotifyService _spotifyService;

    public GetRecommendationsQueryHandler(ISpotifyService spotifyService)
    {
        _spotifyService = spotifyService;
    }

    public async Task<RecommendationsResponse> Handle(GetRecommendationsQuery request, CancellationToken cancellationToken)
    {
        return await _spotifyService.GetRecommendations(request, cancellationToken);
    }
}