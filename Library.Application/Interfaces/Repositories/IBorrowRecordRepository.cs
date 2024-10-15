using Library.Domain;

namespace Library.Application.Interfaces.Repositories;

public interface IBorrowRecordRepository: IBaseRepository<BorrowRecord>
{
    Task<BorrowRecord?> GetBorrowRecordByBookIdAsync(Guid bookId, CancellationToken token);
}