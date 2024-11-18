using FluentAssertions;
using Library.Application.Authors.Commands.CreateAuthor;
using Library.Domain;
using Library.Tests.Common;
using Library.Tests.Common.Mocks;
using Moq;
using Xunit;

namespace Library.Tests.Tests.Author.CommandsTests;

public class CreateAuthorCommandHandlerTests: BaseTestCommand
{
    private readonly CreateAuthorCommandHandler _handler;
    private readonly AuthorMocks _mocks;

    public CreateAuthorCommandHandlerTests()
    {
        _mocks = new AuthorMocks(Context.UnitOfWorkMock);
        _handler = new CreateAuthorCommandHandler(Context.UnitOfWorkMock.Object, _mocks._mapperMock.Object);
    }
    
    [Fact]
    public async Task CreateAuthorCommandHandler_Success()
    {
        //Arrange
        var command = new CreateAuthorCommand
        {
            AuthorFirstname = "Some",
            AuthorLastname = "Author",
            AuthorBirthday = new DateTime(1990, 1, 1),
            AuthorCountry = "Some Country",
            Books = new List<Book>()
        };
        
        var cancellationToken = new CancellationToken();
        
        _mocks.SetupAddAuthorAsync(command, new Domain.Author
        {
            AuthorFirstname = command.AuthorFirstname,
            AuthorLastname = command.AuthorLastname,
            AuthorBirthday = command.AuthorBirthday,
            AuthorCountry = command.AuthorCountry,
        }, cancellationToken);

        Context.SetupSaveChangesAsync(1, cancellationToken);

        // Act
        var result = await _handler.Handle(command, cancellationToken);

        // Assert
        _mocks.AuthorRepositoryMock.Verify(repo => repo.AddEntityAsync(It.Is<Domain.Author>(a =>
            a.AuthorFirstname == command.AuthorFirstname &&
            a.AuthorLastname == command.AuthorLastname &&
            a.AuthorBirthday == command.AuthorBirthday &&
            a.AuthorCountry == command.AuthorCountry), cancellationToken), Times.Once);
        
        Context.UnitOfWorkMock.Verify(uow => uow.SaveChangesAsync(cancellationToken), Times.Once);

        result.Should().NotBe(Guid.Empty);
    }
}