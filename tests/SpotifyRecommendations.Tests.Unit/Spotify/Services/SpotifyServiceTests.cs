using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Extensions.Options;
using Moq;
using SpotifyRecommendations.Application.Spotify.Interfaces;
using SpotifyRecommendations.Application.Spotify.Options;
using SpotifyRecommendations.Application.Spotify.Queries.GetRecommendationsQuery;
using SpotifyRecommendations.Application.Spotify.Queries.SearchQuery;
using SpotifyRecommendations.Infrastructure.Spotify.Models;
using SpotifyRecommendations.Infrastructure.Spotify.Services;
using Xunit;

namespace SpotifyRecommendations.Tests.Unit.Spotify.Services;

public class SpotifyServiceTests
{
    private readonly SpotifyService _sut;

    private readonly Mock<ISpotifyRepository> _spotifyRepository;
    private readonly Mock<IOptions<SpotifyOptions>> _options;
    
    public SpotifyServiceTests()
    {
        _spotifyRepository = new Mock<ISpotifyRepository>();
        _options = new Mock<IOptions<SpotifyOptions>>();

        _sut = new SpotifyService(_spotifyRepository.Object, _options.Object);
    }
    
    [Fact]
    public async Task GetGenres_GivenGenresFromRepository_ShouldReturnGenres()
    {
        // Arrange
        const string genreEndpoint = "/genre/test/endpoint";
        var spotifyOptions = new SpotifyOptions { Endpoints = new SpotifyOptions.SpotifyEndpoints { GetGenres = genreEndpoint } };
        _options.Setup(x => x.Value).Returns(spotifyOptions);

        var genresResponseDto = new GenresResponseDto
        {
            Genres = new List<string> { "genre1", "genre2" }
        };
        
        _spotifyRepository
            .Setup(x => x.Get<GenresResponseDto>(genreEndpoint, It.IsAny<CancellationToken>()))
            .ReturnsAsync(genresResponseDto);

        // Act
        var response = await _sut.GetGenres(CancellationToken.None);

        // Assert
        using (new AssertionScope())
        {
            response.Genres.Should().NotBeEmpty();
            response.Should().BeEquivalentTo(genresResponseDto);
        }
    }
    
    [Fact]
    public async Task GetRecommendations_GivenValidInput_ShouldReturnRecommendations()
    {
        // Arrange
        const string recommendationEndpoint = "/recommendation/test/endpoint";
        var spotifyOptions = new SpotifyOptions { Endpoints = new SpotifyOptions.SpotifyEndpoints { GetRecommendations = recommendationEndpoint } };
        _options.Setup(x => x.Value).Returns(spotifyOptions);

        var recommendationResponseDto = new RecommendationsResponseDto
        {
            Tracks = new List<TrackDto>
            {
                new()
                {
                    Name = "Test 1",
                    Id = "123",
                    Album = new AlbumDto
                    {
                        Name = "Album 1",
                        Images = new[] { new AlbumDto.ImageDto{ Height = 30, Width = 30, Url = "url"}}
                    },
                    Artists = new[]{ new ArtistDto{ Name = "Artist 1" }}
                }
            },
            Seeds = new List<RecommendationsResponseDto.SeedDto>
            {
                new() { InitialPoolSize = 100 }
            }
        };
        
        _spotifyRepository
            .Setup(x => x.Get<RecommendationsResponseDto>(It.Is<string>(xx => xx.StartsWith(recommendationEndpoint)), It.IsAny<CancellationToken>()))
            .ReturnsAsync(recommendationResponseDto);

        var request = new GetRecommendationsQuery { TrackIds = new List<string?>{ "987" } };

        // Act
        var response = await _sut.GetRecommendations(request, CancellationToken.None);

        // Assert
        using (new AssertionScope())
        {
            response.Tracks.Should().NotBeEmpty();
            response.TotalTracks.Should().Be(100);
        }
    }
    
    [Fact]
    public async Task Search_GivenValidInput_ShouldReturnSearchResults()
    {
        // Arrange
        const string searchEndpoint = "/search/test/endpoint";
        var spotifyOptions = new SpotifyOptions { Endpoints = new SpotifyOptions.SpotifyEndpoints { SearchRequest = searchEndpoint } };
        _options.Setup(x => x.Value).Returns(spotifyOptions);

        var searchResponseDto = new SearchResponseDto
        {
            Tracks = new TrackListDto
            {
                Items = new List<TrackDto?>
                {
                    new()
                    {
                        Name = "Test 1",
                        Id = "123",
                        Album = new AlbumDto
                        {
                            Name = "Album 1",
                            Images = new[] { new AlbumDto.ImageDto{ Height = 30, Width = 30, Url = "url"}}
                        },
                        Artists = new[]{ new ArtistDto{ Name = "Artist 1" }}
                    }
                }
            }
        };
        
        _spotifyRepository
            .Setup(x => x.Get<SearchResponseDto>(It.Is<string>(xx => xx.StartsWith(searchEndpoint)), It.IsAny<CancellationToken>()))
            .ReturnsAsync(searchResponseDto);

        var request = new SearchQuery { QueryString = "test" };

        // Act
        var response = await _sut.Search(request, CancellationToken.None);

        // Assert
        using (new AssertionScope())
        {
            response.Tracks.Should().NotBeEmpty();
            response.Tracks.Count.Should().Be(1);
        }
    }
}