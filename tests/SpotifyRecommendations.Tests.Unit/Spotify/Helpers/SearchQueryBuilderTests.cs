using System.Collections;
using FluentAssertions;
using SpotifyRecommendations.Application.Spotify.Queries.GetRecommendationsQuery;
using SpotifyRecommendations.Application.Spotify.Queries.SearchQuery;
using SpotifyRecommendations.Infrastructure.Spotify.Helpers;
using Xunit;

namespace SpotifyRecommendations.Tests.Unit.Spotify.Helpers;

public class SearchQueryBuilderTests
{
    [Theory]
    [ClassData(typeof(SearchQueryData))]
    public void BuildSearchQuery_GivenSearchQueryModel_ShouldReturnStringContainingExpected(SearchQuery searchQuery, string[] expected)
    {
        // Act
        var result = QueryBuilder.BuildSearchQuery(searchQuery);

        // Assert
        result.Should().ContainAll(expected);
    }
    
    [Theory]
    [ClassData(typeof(GetRecommendationsQueryData))]
    public void BuildRecommendationsQuery_GivenRecommendationQuery_ShouldReturnStringContainingExpected(GetRecommendationsQuery query, string[] expected)
    {
        // Act
        var result = QueryBuilder.BuildRecommendationsQuery(query);

        // Assert
        result.Should().ContainAll(expected);
    }
}

public class SearchQueryData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new SearchQuery { Artist = "artist1" },
            new []{ "+artist:artist1" }
        };
        yield return new object[]
        {
            new SearchQuery { Album = "testalbum", YearStart = "1990"},
            new []{ "+album:testalbum", "+year:1990" }
        };
        yield return new object[]
        {
            new SearchQuery { YearStart = "1993", YearEnd = "2001", Genre = "pop"},
            new []{ "+year:1993-2001", "+genre:pop" }
        };
        yield return new object[]
        {
            new SearchQuery { TagNew = true, TagHipster = true},
            new []{ "+tag:new", "+tag:hipster" }
        };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class GetRecommendationsQueryData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new GetRecommendationsQuery { TrackIds = new []{ "123", "234", "345" }},
            new []{ "&seed_tracks=123,234,345" }
        };
        yield return new object[]
        {
            new GetRecommendationsQuery { TargetDanceability = 5, TargetAcousticness = 3, TargetEnergy = 6, TargetPopularity = 4},
            new []{ "&target_danceability=0.5", "&target_acousticness=0.3", "&target_energy=0.6", "&target_popularity=40" }
        };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}