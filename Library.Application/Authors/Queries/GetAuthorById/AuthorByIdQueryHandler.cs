using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Application.Common.Exceptions;
using Library.Application.Interfaces;
using Library.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Authors.Queries.GetAuthorDetails;

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