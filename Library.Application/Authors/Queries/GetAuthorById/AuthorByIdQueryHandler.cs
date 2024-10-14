using Library.Application.Interfaces;
using MediatR;

namespace Library.Application.Authors.Queries.GetAuthorById;

public class AuthorByIdQueryHandler: IRequestHandler<GetAuthorByIdQuery, AuthorDetailsVm>
{
    private readonly IUnitOfWork _unitOfWork;

    public AuthorByIdQueryHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;
    
    public async Task<AuthorDetailsVm> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Authors.GetAuthorInfoByIdAsync(request.author_id, cancellationToken);
    }
}