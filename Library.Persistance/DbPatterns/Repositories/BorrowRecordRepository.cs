using Library.Application.Interfaces.Repositories;
using Library.Domain;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistance.DbPatterns.Repositories;

public class BorrowRecordRepository: BaseRepository<BorrowRecord>, IBorrowRecordRepository
{
    public BorrowRecordRepository(LibraryDBContext libraryDbContext) : base(libraryDbContext){ }

    public async Task<BorrowRecord?> GetBorrowRecordByBookIdAsync(Guid bookId, CancellationToken token)
    {
        return await _libraryDbContext.BorrowRecords
            .FirstOrDefaultAsync(br => br.bookId == bookId, token);
    }

    public async Task<List<Book>> GetExpiringRecordsAsync(string userId, CancellationToken token)
    {
        var currentDate = DateTime.UtcNow;

        return await _libraryDbContext.books
            .Include(book => book.borrowRecords)
            .Where(book => book.borrowRecords.Any(record =>
                record.userId == userId &&
                record.book_issue_expiration_date > currentDate && 
                record.book_issue_expiration_date <= currentDate.AddDays(1)))
            .ToListAsync(token);
    }
}