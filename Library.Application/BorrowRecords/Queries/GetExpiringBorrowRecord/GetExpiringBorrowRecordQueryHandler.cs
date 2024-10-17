using Library.Application.Common.Exceptions;
using Library.Application.Interfaces;
using Library.Domain;
using MediatR;

namespace Library.Application.BorrowRecords.Queries.GetExpiringBorrowRecord;

public class GetExpiringBorrowRecordQueryHandler: IRequestHandler<GetExpiringBorrowRecordQuery, ExpiringRecordVm>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetExpiringBorrowRecordQueryHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;
    
    public async Task<ExpiringRecordVm> Handle(GetExpiringBorrowRecordQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.FindUserByEmail(request.Email);
        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.Email);
        }
        
        return await _unitOfWork.BorrowRecords.GetExpiringRecordsAsync(user.Id, cancellationToken);
    }
}