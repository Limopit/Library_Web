using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Application.Authors.Queries.GetAuhtorList;
using Library.Application.Common.Exceptions;
using Library.Application.Interfaces;
using Library.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Authors.Queries.GetAuthorBooksList;

public class GetAuthorBooksListQueryHandler: IRequestHandler<GetAuthorBooksListQuery, AuthorBooksListVm>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAuthorBooksListQueryHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task<AuthorBooksListVm> Handle(GetAuthorBooksListQuery request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.Authors.GetAuthorByIdAsync(request.author_id, cancellationToken) == null)
        {
            throw new NotFoundException(nameof(Author), request.author_id);
        }
        return await _unitOfWork.Authors.GetAuthorBookListAsync(request.author_id, cancellationToken);
    }
}