using System.Globalization;
using System.Text;
using SpotifyRecommendations.Application.Spotify.Queries.GetRecommendationsQuery;
using SpotifyRecommendations.Application.Spotify.Queries.SearchQuery;

namespace SpotifyRecommendations.Infrastructure.Spotify.Helpers;

public static class QueryBuilder
{
    public static string BuildSearchQuery(SearchQuery searchQuery)
    {
        var searchQueryString = new StringBuilder();

        if (!string.IsNullOrWhiteSpace(searchQuery.QueryString))
            searchQueryString.Append($"{searchQuery.QueryString}");
        
        if (!string.IsNullOrWhiteSpace(searchQuery.Artist))
            searchQueryString.Append($"+artist:{searchQuery.Artist}");

        if (!string.IsNullOrWhiteSpace(searchQuery.Album))
            searchQueryString.Append($"+album:{searchQuery.Album}");
        
        if (!string.IsNullOrWhiteSpace(searchQuery.YearStart) && !string.IsNullOrWhiteSpace(searchQuery.YearEnd))
            searchQueryString.Append($"+year:{searchQuery.YearStart}-{searchQuery.YearEnd}");
        
        if (!string.IsNullOrWhiteSpace(searchQuery.YearStart) && string.IsNullOrWhiteSpace(searchQuery.YearEnd))
            searchQueryString.Append($"+year:{searchQuery.YearStart}");
        
        if (!string.IsNullOrWhiteSpace(searchQuery.Genre))
            searchQueryString.Append($"+genre:{searchQuery.Genre}");
        
        if (searchQuery.TagNew)
            searchQueryString.Append($"+tag:new");
        
        if (searchQuery.TagHipster)
            searchQueryString.Append($"+tag:hipster");
        
        return searchQueryString.ToString();
    }

    public static string BuildRecommendationsQuery(GetRecommendationsQuery getRecommendationsQuery)
    {
        var queryString = new StringBuilder();

        if (getRecommendationsQuery.TrackIds.Any())
            queryString.Append($"&seed_tracks={string.Join(",", getRecommendationsQuery.TrackIds)}");

        if (getRecommendationsQuery.TargetDanceability is not null)
            queryString.Append($"&target_danceability={((double)getRecommendationsQuery.TargetDanceability / 10).ToString(CultureInfo.InvariantCulture)}");

        if (getRecommendationsQuery.TargetAcousticness is not null)
            queryString.Append($"&target_acousticness={((double)getRecommendationsQuery.TargetAcousticness / 10).ToString(CultureInfo.InvariantCulture)}");
        
        if (getRecommendationsQuery.TargetEnergy is not null)
            queryString.Append($"&target_energy={((double)getRecommendationsQuery.TargetEnergy / 10).ToString(CultureInfo.InvariantCulture)}");
        
        if (getRecommendationsQuery.TargetPopularity is not null)
            queryString.Append($"&target_popularity={getRecommendationsQuery.TargetPopularity * 10}");

        return queryString.ToString();
    }
}