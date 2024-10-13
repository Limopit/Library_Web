using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Authors.Queries.GetAuhtorList;

public class GetAuthorListQueryHandler: IRequestHandler<GetAuthorListQuery, AuthorListVm>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAuthorListQueryHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;
    
    public async Task<AuthorListVm> Handle(GetAuthorListQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Authors.GetAuthorListAsync(cancellationToken);
    }
}