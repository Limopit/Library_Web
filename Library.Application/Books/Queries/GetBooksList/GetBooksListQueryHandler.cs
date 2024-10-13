using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Application.Authors.Queries.GetAuhtorList;
using Library.Application.Authors.Queries.GetAuthorDetails;
using Library.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Books.Queries;

public class GetBooksListQueryHandler: IRequestHandler<GetBooksListQuery, BooksListVm>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetBooksListQueryHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;
    
    public async Task<BooksListVm> Handle(GetBooksListQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Books.GetBookListAsync(cancellationToken);
    }
}