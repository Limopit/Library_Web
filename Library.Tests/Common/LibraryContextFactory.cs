using System.Linq.Expressions;
using Library.Application.Interfaces;
using Library.Application.Interfaces.Repositories;
using Library.Domain;
using Library.Persistance;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Library.Tests.Common;

public class LibraryContextFactory
{
    public readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public LibraryContextFactory()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
    }

    public void SetupSaveChangesAsync(int result = 1, CancellationToken cancellationToken = default)
    {
        _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync(cancellationToken))
            .ReturnsAsync(result);
    }

}