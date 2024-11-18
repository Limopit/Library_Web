using FluentAssertions;
using Library.Application.Authors.Commands.UpdateAuthor;
using Library.Application.Common.Exceptions;
using Library.Domain;
using Library.Tests.Common;
using Library.Tests.Common.Mocks;
using Xunit;

namespace Library.Tests.Tests.Author.CommandsTests;

public class UpdateAuthorCommandHandlerTests: BaseTestCommand
{
    private readonly UpdateAuthorCommandHandler _handler;
    private readonly AuthorMocks _mocks;

    public UpdateAuthorCommandHandlerTests()
    {
        _mocks = new AuthorMocks(Context.UnitOfWorkMock);
        _handler = new UpdateAuthorCommandHandler(Context.UnitOfWorkMock.Object, _mocks._mapperMock.Object);
    }

    [Fact]
    public async Task UpdateAuthorTest_Success()
    {
        // Arrange
        var authorId = Guid.NewGuid();
        var existingAuthor = new Domain.Author
        {
            AuthorId = authorId,
            AuthorFirstname = "Some",
            AuthorLastname = "Author",
            AuthorCountry = "Some Country",
            AuthorBirthday = new DateTime(1980, 1, 1),
            Books = new List<Book>()
        };

        CancellationToken token = new CancellationToken();

        // Настройка мока для получения автора
        _mocks.SetupGetAuthorByIdAsync(authorId, existingAuthor);

        var command = new UpdateAuthorCommand
        {
            AuthorId = authorId,
            AuthorFirstname = "UpdatedFirstName",
            AuthorLastname = "UpdatedLastName",
            AuthorCountry = "UpdatedCountry",
            AuthorBirthday = new DateTime(1990, 1, 1)
        };

        // Настройка мока маппера
        _mocks.SetupMapperForUpdate(command, existingAuthor);

        // Act
        await _handler.Handle(command, token);

        // Assert
        existingAuthor.AuthorFirstname.Should().Be("UpdatedFirstName");
        existingAuthor.AuthorLastname.Should().Be("UpdatedLastName");
        existingAuthor.AuthorCountry.Should().Be("UpdatedCountry");
        existingAuthor.AuthorBirthday.Should().Be(new DateTime(1990, 1, 1));
    }

    
    [Fact]
    public async Task UpdateAuthorTest_AuthorNotFound()
    {
        //Arrange
        var authorId = Guid.NewGuid();
        var expectedAuthor = new Domain.Author
        {
            AuthorId = authorId,
            AuthorFirstname = "Some",
            AuthorLastname = "Author",
            AuthorCountry = "Some Country",
            Books = new List<Book>()
        };

        CancellationToken token = new CancellationToken();

        _mocks.SetupGetAuthorByIdAsync(authorId, expectedAuthor);

        //Act & Assert
        var command = new UpdateAuthorCommand
        {
            AuthorFirstname = "Not",
            AuthorLastname = "Author"
        };

        await _handler
            .Invoking(async h => await h.Handle(command, CancellationToken.None))
            .Should()
            .ThrowAsync<NotFoundException>();
    }
}