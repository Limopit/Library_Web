using Library.Domain;

namespace Library.Application.Interfaces;

public interface IBorrowRecordRepository
{
    Task AddRecordAsync(BorrowRecord record, CancellationToken token);
}