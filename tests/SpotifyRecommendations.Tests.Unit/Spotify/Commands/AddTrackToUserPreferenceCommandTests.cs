using FluentAssertions;
using FluentAssertions.Execution;
using Moq;
using SpotifyRecommendations.Application.Spotify.Commands.AddTrackToUserPreferenceCommand;
using SpotifyRecommendations.Application.Spotify.Interfaces;
using SpotifyRecommendations.Application.Spotify.Models;
using Xunit;

namespace SpotifyRecommendations.Tests.Unit.Spotify.Commands;

public class AddTrackToUserPreferenceCommandTests
{
    [Fact]
    public async Task Handle_GivenValidRequest_ShouldAddTrackToUserPreference()
    {
        // Arrange
        var userPreferenceService = new Mock<IUserPreferenceService>();
        var sut = new AddTrackToUserPreferenceCommandHandler(userPreferenceService.Object);

        var track = new Track { Id = "testId" };
        var request = new AddTrackToUserPreferenceCommand
        {
            Track = track
        };

        // Act
        var result = await sut.Handle(request, CancellationToken.None);

        // Assert
        using (new AssertionScope())
        {
            result.Succeeded.Should().BeTrue();
            userPreferenceService.Verify(x => x.AddTrack(track), Times.Once);
        }
    }
}