using System.Text;
using SpotifyRecommendations.Application.Spotify.Queries.SearchQuery;

namespace SpotifyRecommendations.Infrastructure.Spotify.Helpers;

public static class SearchQueryBuilder
{
    public static string Build(SearchQuery searchQuery)
    {
        var searchQueryString = new StringBuilder();
        
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
}