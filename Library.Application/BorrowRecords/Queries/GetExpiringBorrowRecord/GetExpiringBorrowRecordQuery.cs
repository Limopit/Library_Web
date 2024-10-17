using MediatR;

namespace Library.Application.BorrowRecords.Queries.GetExpiringBorrowRecord;

public class GetExpiringBorrowRecordQuery: IRequest<ExpiringRecordVm>
{
    public string Email { get; set; }
}