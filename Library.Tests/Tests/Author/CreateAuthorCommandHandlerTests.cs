using FluentAssertions;
using Library.Application.Authors.Commands.CreateAuthor;
using Library.Domain;
using Library.Tests.Common;
using Library.Tests.Common.Mocks;
using Moq;
using Xunit;

namespace Library.Tests.Tests.Author;

public class CreateAuthorCommandHandlerTests: BaseTestCommand
{
    private readonly CreateAuthorCommandHandler _handler;
    private readonly AuthorMocks _mocks;

    public CreateAuthorCommandHandlerTests()
    {
        _handler = new CreateAuthorCommandHandler(Context.UnitOfWorkMock.Object);
        _mocks = new AuthorMocks(Context.UnitOfWorkMock);
    }
    
    [Fact]
    public async Task CreateAuthorCommandHandler_Success()
    {
        //Arrange
        var command = new CreateAuthorCommand
        {
            author_firstname = "Some",
            author_lastname = "Author",
            author_birthday = new DateTime(1990, 1, 1),
            author_country = "Some Country",
            books = new List<Book>()
        };
        
        var cancellationToken = new CancellationToken();
        
        _mocks.SetupAddAuthorAsync(new Domain.Author
        {
            author_firstname = command.author_firstname,
            author_lastname = command.author_lastname,
            author_birthday = command.author_birthday,
            author_country = command.author_country,
        }, cancellationToken);

        Context.SetupSaveChangesAsync(1, cancellationToken);

        // Act
        var result = await _handler.Handle(command, cancellationToken);

        // Assert
        _mocks.AuthorRepositoryMock.Verify(repo => repo.AddEntityAsync(It.Is<Domain.Author>(a =>
            a.author_firstname == command.author_firstname &&
            a.author_lastname == command.author_lastname &&
            a.author_birthday == command.author_birthday &&
            a.author_country == command.author_country), cancellationToken), Times.Once);
        
        Context.UnitOfWorkMock.Verify(uow => uow.SaveChangesAsync(cancellationToken), Times.Once);

        result.Should().NotBe(Guid.Empty);
    }
}