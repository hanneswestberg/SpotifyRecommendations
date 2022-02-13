using FluentAssertions;
using FluentAssertions.Execution;
using Moq;
using SpotifyRecommendations.Application.Spotify.Commands.RemoveTrackFromUserPreferenceCommand;
using SpotifyRecommendations.Application.Spotify.Interfaces;
using SpotifyRecommendations.Application.Spotify.Models;
using Xunit;

namespace SpotifyRecommendations.Tests.Unit.Spotify.Commands;

public class RemoveTrackToUserPreferenceCommandTests
{
    [Fact]
    public async Task Handle_GivenValidRequest_ShouldRemoveTrackFromUserPreference()
    {
        // Arrange
        var userPreferenceService = new Mock<IUserPreferenceService>();
        var sut = new RemoveTrackFromUserPreferenceCommandHandler(userPreferenceService.Object);

        var track = new Track { Id = "testId" };
        var request = new RemoveTrackFromUserPreferenceCommand
        {
            Track = track
        };

        // Act
        var result = await sut.Handle(request, CancellationToken.None);

        // Assert
        using (new AssertionScope())
        {
            result.Succeeded.Should().BeTrue();
            userPreferenceService.Verify(x => x.RemoveTrack(track), Times.Once);
        }
    }
}