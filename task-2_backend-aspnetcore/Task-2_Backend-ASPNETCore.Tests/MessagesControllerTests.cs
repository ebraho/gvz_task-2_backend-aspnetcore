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

    #region GetMessages

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
        result.AdministrativeChangeMessages.Should().BeEmpty();
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
        await _client.PostAsJsonAsync(
            "/messages/administrative-change",
            ValidAdministrativeChangeMessageCreateDto()
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
        result.AdministrativeChangeMessages.Should().HaveCount(1);
        result.AdministrativeChangeMessages.First().LastName.Should().Be("Last");
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

    #endregion

    #region PostQuestionMessage

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

    #endregion

    #region PostAdministrativeChangeMessage

    [Fact]
    public async Task PostAdministrativeChangeMessage_ReturnsCreatedMessage_WithValidData()
    {
        // Arrange
        await CleanDatabase();

        // Act
        var response = await _client.PostAsJsonAsync(
            "/messages/administrative-change",
            ValidAdministrativeChangeMessageCreateDto()
        );

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<AdministrativeChangeMessageDto>();
        result.Should().NotBeNull();
        result.Id.Should().BeGreaterThan(0);
        result.FirstName.Should().Be("First");
        result.LastName.Should().Be("Last");
        result.PostalAddress.Street.Should().Be("Haspelstrasse");
        result.PostalAddress.HouseNumber.Should().Be(5);
        result.PostalAddress.ZipCode.Should().Be("8041");
        result.PostalAddress.City.Should().Be("Zürich");
        result.PostalAddress.Canton.Should().Be("Zürich");
        result.FreeText.Should().Be("Address change requested.");
    }

    [Fact]
    public async Task PostAdministrativeChangeMessage_ReturnsBadRequest_WithMissingFirstName()
    {
        // Arrange
        await CleanDatabase();
        var dto = ValidAdministrativeChangeMessageCreateDto();
        dto.FirstName = "";

        // Act
        var response = await _client.PostAsJsonAsync("/messages/administrative-change", dto);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task PostQuestionMessage_ReturnsBadRequest_WithTooLongFirstName()
    {
        // Arrange
        await CleanDatabase();
        var dto = ValidAdministrativeChangeMessageCreateDto();
        dto.FirstName = new string('a', 101);

        // Act
        var response = await _client.PostAsJsonAsync("/messages/administrative-change", dto);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task PostAdministrativeChangeMessage_ReturnsBadRequest_WithMissingLastName()
    {
        // Arrange
        await CleanDatabase();
        var dto = ValidAdministrativeChangeMessageCreateDto();
        dto.LastName = "";

        // Act
        var response = await _client.PostAsJsonAsync("/messages/administrative-change", dto);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task PostQuestionMessage_ReturnsBadRequest_WithTooLongLastName()
    {
        // Arrange
        await CleanDatabase();
        var dto = ValidAdministrativeChangeMessageCreateDto();
        dto.LastName = new string('a', 101);

        // Act
        var response = await _client.PostAsJsonAsync("/messages/administrative-change", dto);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task PostAdministrativeChangeMessage_ReturnsBadRequest_WithMissingStreet()
    {
        // Arrange
        await CleanDatabase();
        var dto = ValidAdministrativeChangeMessageCreateDto();
        dto.PostalAddress.Street = "";

        // Act
        var response = await _client.PostAsJsonAsync("/messages/administrative-change", dto);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task PostQuestionMessage_ReturnsBadRequest_WithTooLongStreet()
    {
        // Arrange
        await CleanDatabase();
        var dto = ValidAdministrativeChangeMessageCreateDto();
        dto.PostalAddress.Street = new string('a', 101);

        // Act
        var response = await _client.PostAsJsonAsync("/messages/administrative-change", dto);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task PostAdministrativeChangeMessage_ReturnsBadRequest_WithZeroHouseNumber()
    {
        // Arrange
        await CleanDatabase();
        var dto = ValidAdministrativeChangeMessageCreateDto();
        dto.PostalAddress.HouseNumber = 0;

        // Act
        var response = await _client.PostAsJsonAsync("/messages/administrative-change", dto);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task PostAdministrativeChangeMessage_ReturnsBadRequest_WithNegativeHouseNumber()
    {
        // Arrange
        await CleanDatabase();
        var dto = ValidAdministrativeChangeMessageCreateDto();
        dto.PostalAddress.HouseNumber = -1;

        // Act
        var response = await _client.PostAsJsonAsync("/messages/administrative-change", dto);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task PostAdministrativeChangeMessage_ReturnsBadRequest_WithInvalidZipCode()
    {
        // Arrange
        await CleanDatabase();
        var dto = ValidAdministrativeChangeMessageCreateDto();
        dto.PostalAddress.ZipCode = "abc";

        // Act
        var response = await _client.PostAsJsonAsync("/messages/administrative-change", dto);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task PostAdministrativeChangeMessage_ReturnsBadRequest_WithMissingCity()
    {
        // Arrange
        await CleanDatabase();
        var dto = ValidAdministrativeChangeMessageCreateDto();
        dto.PostalAddress.City = "";

        // Act
        var response = await _client.PostAsJsonAsync("/messages/administrative-change", dto);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task PostQuestionMessage_ReturnsBadRequest_WithTooLongCity()
    {
        // Arrange
        await CleanDatabase();
        var dto = ValidAdministrativeChangeMessageCreateDto();
        dto.PostalAddress.City = new string('a', 101);

        // Act
        var response = await _client.PostAsJsonAsync("/messages/administrative-change", dto);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task PostAdministrativeChangeMessage_ReturnsBadRequest_WithMissingCanton()
    {
        // Arrange
        await CleanDatabase();
        var dto = ValidAdministrativeChangeMessageCreateDto();
        dto.PostalAddress.Canton = "";

        // Act
        var response = await _client.PostAsJsonAsync("/messages/administrative-change", dto);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task PostQuestionMessage_ReturnsBadRequest_WithTooLongCanton()
    {
        // Arrange
        await CleanDatabase();
        var dto = ValidAdministrativeChangeMessageCreateDto();
        dto.PostalAddress.Canton = new string('a', 101);

        // Act
        var response = await _client.PostAsJsonAsync("/messages/administrative-change", dto);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task PostAdministrativeChangeMessage_ReturnsBadRequest_WithMissingFreeText()
    {
        // Arrange
        await CleanDatabase();
        var dto = ValidAdministrativeChangeMessageCreateDto();
        dto.FreeText = "";

        // Act
        var response = await _client.PostAsJsonAsync("/messages/administrative-change", dto);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task PostAdministrativeChangeMessage_ReturnsBadRequest_WithTooLongFreeText()
    {
        // Arrange
        await CleanDatabase();
        var dto = ValidAdministrativeChangeMessageCreateDto();
        dto.FreeText = new string('a', 1025);

        // Act
        var response = await _client.PostAsJsonAsync("/messages/administrative-change", dto);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task PostAdministrativeChangeMessage_ReturnsJsonContentType()
    {
        // Arrange
        await CleanDatabase();

        // Act
        var response = await _client.PostAsJsonAsync(
            "/messages/administrative-change",
            ValidAdministrativeChangeMessageCreateDto()
        );

        // Assert
        response.Content.Headers.ContentType.Should().NotBeNull();
        response.Content.Headers.ContentType!.MediaType.Should().Be("application/json");
    }

    #endregion

    private static AdministrativeChangeMessageCreateDto ValidAdministrativeChangeMessageCreateDto() =>
    new()
    {
        FirstName = "First",
        LastName = "Last",
        PostalAddress = new PostalAddress
        {
            Street = "Haspelstrasse",
            HouseNumber = 5,
            ZipCode = "8041",
            City = "Zürich",
            Canton = "Zürich"
        },
        FreeText = "Address change requested."
    };

    private async Task CleanDatabase()
    {
        using var scope = factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<MessagesContext>();
        context.QuestionMessages.RemoveRange(context.QuestionMessages);
        context.AdministrativeChangeMessages.RemoveRange(context.AdministrativeChangeMessages);
        await context.SaveChangesAsync();
    }
}
