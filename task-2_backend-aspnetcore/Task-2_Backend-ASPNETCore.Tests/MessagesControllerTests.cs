using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using GVZ.Task2BackendASPNETCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Task2BackendASPNETCore.Tests;

public class MessagesControllerTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task GetMessages_ReturnsEmptyList_WhenNoMessagesExist()
    {
        // Arrange
        await CleanDatabase();

        // Act
        var response = await _client.GetAsync("/messages");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<MessageCollectionDto>();
        result.Should().NotBeNull();
        result.QuestionMessages.Should().BeEmpty();
    }

    [Fact]
    public async Task GetMessages_ReturnsAllMessages_WhenMessagesExist()
    {
        // Arrange
        await CleanDatabase();
        await _client.PostAsJsonAsync(
            "/messages/question",
            new QuestionMessageCreateDto { Question = "Test Question 1" }
        );
        await _client.PostAsJsonAsync(
            "/messages/question",
            new QuestionMessageCreateDto { Question = "Test Question 2" }
        );

        // Act
        var response = await _client.GetAsync("/messages");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<MessageCollectionDto>();
        result.Should().NotBeNull();
        result.QuestionMessages.Should().HaveCount(2);
        result.QuestionMessages.Select(m => m.Question).Should().Contain("Test Question 1");
        result.QuestionMessages.Select(m => m.Question).Should().Contain("Test Question 2");
    }

    [Fact]
    public async Task GetMessages_ReturnsJsonContentType()
    {
        // Arrange
        await CleanDatabase();

        // Act
        var response = await _client.GetAsync("/messages");

        // Assert
        response.Content.Headers.ContentType.Should().NotBeNull();
        response.Content.Headers.ContentType.MediaType.Should().Be("application/json");
    }

    [Fact]
    public async Task PostQuestionMessage_ReturnsCreatedMessage_WithValidData()
    {
        // Arrange
        await CleanDatabase();

        // Act
        var response = await _client.PostAsJsonAsync(
            "/messages/question",
            new QuestionMessageCreateDto { Question = "Test Question" }
        );

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<QuestionMessageDto>();
        result.Should().NotBeNull();
        result.Id.Should().BeGreaterThan(0);
        result.Question.Should().Be("Test Question");
    }

    [Fact]
    public async Task PostQuestionMessage_ReturnsBadRequest_WithEmptyQuestion()
    {
        // Arrange
        await CleanDatabase();

        // Act
        var response = await _client.PostAsJsonAsync(
            "/messages/question",
            new QuestionMessageCreateDto { Question = "" }
        );

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task PostQuestionMessage_ReturnsBadRequest_WithTooLongQuestion()
    {
        // Arrange
        await CleanDatabase();

        // Act
        var response = await _client.PostAsJsonAsync(
            "/messages/question",
            new QuestionMessageCreateDto { Question = new string('a', 1025) }
        );

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task PostQuestionMessage_ReturnsJsonContentType()
    {
        // Arrange
        await CleanDatabase();

        // Act
        var response = await _client.PostAsJsonAsync(
            "/messages/question",
            new QuestionMessageCreateDto { Question = "Test Question" }
        );

        // Assert
        response.Content.Headers.ContentType.Should().NotBeNull();
        response.Content.Headers.ContentType.MediaType.Should().Be("application/json");
    }

    private async Task CleanDatabase()
    {
        using var scope = factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<MessagesContext>();
        context.QuestionMessages.RemoveRange(context.QuestionMessages);
        await context.SaveChangesAsync();
    }
}
