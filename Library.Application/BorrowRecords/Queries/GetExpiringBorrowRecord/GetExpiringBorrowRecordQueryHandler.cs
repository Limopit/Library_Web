using AutoMapper;
using Library.Application.Common.Exceptions;
using Library.Application.Interfaces;
using Library.Domain;
using MediatR;

namespace Library.Application.BorrowRecords.Queries.GetExpiringBorrowRecord;

public class GetExpiringBorrowRecordQueryHandler: IRequestHandler<GetExpiringBorrowRecordQuery, ExpiringRecordVm>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetExpiringBorrowRecordQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ExpiringRecordVm> Handle(GetExpiringBorrowRecordQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.FindUserByEmail(request.Email);
        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.Email);
        }

        var expiredBooks = 
            await _unitOfWork.BorrowRecords.GetExpiringRecordsAsync(user.Id, cancellationToken);

        var expiredBookList = _mapper.Map<List<ExpiringBookDto>>(expiredBooks);
        
        return new ExpiringRecordVm { Records = expiredBookList };
        
    }
}