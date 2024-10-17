using Library.Application.Interfaces;
using MediatR;

namespace Library.Application.Authors.Queries.GetAuthorList;

public class GetAuthorListQueryHandler: IRequestHandler<GetAuthorListQuery, AuthorListVm>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAuthorListQueryHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;
    
    public async Task<AuthorListVm> Handle(GetAuthorListQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Authors.GetPaginatedEntityListAsync(
            request.PageNumber, request.PageSize, cancellationToken);
    }
}