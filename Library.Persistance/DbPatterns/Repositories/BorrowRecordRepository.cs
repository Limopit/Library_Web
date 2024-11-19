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
            .FirstOrDefaultAsync(br => br.BookId == bookId, token);
    }

    public async Task<List<Book>> GetExpiringRecordsAsync(string userId, CancellationToken token)
    {
        var currentDate = DateTime.UtcNow;

        return await _libraryDbContext.Books
            .Include(book => book.BorrowRecords)
            .Where(book => book.BorrowRecords.Any(record =>
                record.UserId == userId &&
                record.BookIssueExpirationDate > currentDate && 
                record.BookIssueExpirationDate <= currentDate.AddDays(1)))
            .ToListAsync(token);
    }
}