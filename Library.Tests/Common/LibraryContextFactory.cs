using Library.Application.Interfaces;
using Moq;

namespace Library.Tests.Common;

public class LibraryContextFactory
{
    public readonly Mock<IUnitOfWork> UnitOfWorkMock;

    public LibraryContextFactory()
    {
        UnitOfWorkMock = new Mock<IUnitOfWork>();
    }

    public void SetupSaveChangesAsync(int result = 1, CancellationToken cancellationToken = default)
    {
        UnitOfWorkMock.Setup(uow => uow.SaveChangesAsync(cancellationToken))
            .ReturnsAsync(result);
    }

}