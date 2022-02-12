using FluentAssertions;
using FluentAssertions.Execution;
using SpotifyRecommendations.Infrastructure.Factories;
using Xunit;

namespace SpotifyRecommendations.Tests.Unit.Factories;

public class HttpClientFactoryTest
{
    [Fact]
    public void GetClientByUri_GivenValidInput_ShouldReturnHttpClient()
    {
        // Arrange
        var sut = new HttpClientFactory();
        const string uriString = "https://www.google.se";
        var uri = new Uri(uriString);

        // Act
        var client = sut.GetClientByUri(uri);

        // Assert
        using (new AssertionScope())
        {
            client.Should().NotBeNull();
            client.BaseAddress.Should().BeEquivalentTo(uri);
        }
    }
    
    [Fact]
    public void GetClientByUri_GivenNewUri_WhenAlreadyPopulated_ShouldReturnNewHttpClient()
    {
        // Arrange
        var sut = new HttpClientFactory();
        const string uriString1 = "https://www.google.se";
        const string uriString2= "https://www.facebook.se";
        var uri1 = new Uri(uriString1);
        var uri2 = new Uri(uriString2);

        var existingClient = sut.GetClientByUri(uri1);

        // Act
        var client = sut.GetClientByUri(uri2);

        // Assert
        client.Should().NotBeSameAs(existingClient);
    }

    [Fact]
    public void GetClientByUri_GivenExistingUri_ShouldReturnExistingHttpClient()
    {
        // Arrange
        var sut = new HttpClientFactory();
        const string uriString = "https://www.google.se";
        var uri = new Uri(uriString);

        var existingClient = sut.GetClientByUri(uri);

        // Act
        var client = sut.GetClientByUri(uri);

        // Assert
        client.Should().BeSameAs(existingClient);
    }
}