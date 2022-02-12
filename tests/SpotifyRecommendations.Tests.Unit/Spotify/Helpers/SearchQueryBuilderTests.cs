using System.Collections;
using FluentAssertions;
using SpotifyRecommendations.Application.Spotify.Queries.SearchQuery;
using SpotifyRecommendations.Infrastructure.Spotify.Helpers;
using Xunit;

namespace SpotifyRecommendations.Tests.Unit.Spotify.Helpers;

public class SearchQueryBuilderTests
{
    [Theory]
    [ClassData(typeof(SearchQueryData))]
    public void Build_GivenSearchQueryModel_ShouldReturnStringContainingExpected(SearchQuery searchQuery, string[] expected)
    {
        // Act
        var result = SearchQueryBuilder.Build(searchQuery);

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