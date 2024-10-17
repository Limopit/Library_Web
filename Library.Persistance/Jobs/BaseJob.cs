using Library.Application.Interfaces;
using MediatR;

namespace Library.Persistance.Jobs;

public class BaseJob
{
    protected readonly IUnitOfWork _unitOfWork;

    public BaseJob(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
}