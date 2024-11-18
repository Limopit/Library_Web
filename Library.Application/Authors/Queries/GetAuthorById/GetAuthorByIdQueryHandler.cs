using Library.Application.Common.Exceptions;
using Library.Application.Common.Mappings;
using Library.Application.Interfaces;
using Library.Domain;
using MediatR;

namespace Library.Application.Authors.Queries.GetAuthorById;

public class GetAuthorByIdQueryHandler: IRequestHandler<GetAuthorByIdQuery, AuthorDetailsDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapperService _mapper;

    public GetAuthorByIdQueryHandler(IUnitOfWork unitOfWork, IMapperService mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AuthorDetailsDto> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        var authorInfo = await _unitOfWork.Authors.GetAuthorInfoByIdAsync(request.AuthorId, cancellationToken);
        
        if (authorInfo == null)
        {
            throw new NotFoundException(nameof(Author), request.AuthorId);
        }
        
        return await _mapper.Map<Author, AuthorDetailsDto>(authorInfo);
    }
}