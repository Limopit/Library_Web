using AutoMapper;
using Library.Application.Common.Exceptions;
using Library.Application.Interfaces;
using Library.Domain;
using MediatR;

namespace Library.Application.Authors.Queries.GetAuthorById;

public class GetAuthorByIdQueryHandler: IRequestHandler<GetAuthorByIdQuery, AuthorDetailsVm>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAuthorByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AuthorDetailsVm> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        var authorInfo = await _unitOfWork.Authors.GetAuthorInfoByIdAsync(request.author_id, cancellationToken);
        
        if (authorInfo == null)
        {
            throw new NotFoundException(nameof(Author), request.author_id);
        }
        
        return _mapper.Map<AuthorDetailsVm>(authorInfo);
    }
}