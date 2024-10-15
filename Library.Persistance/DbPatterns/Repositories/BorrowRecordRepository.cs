using Library.Application.Interfaces;
using Library.Application.Interfaces.Repositories;
using Library.Domain;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistance.DbPatterns.Repositories;

public class BorrowRecordRepository: BaseRepository<BorrowRecord>, IBorrowRecordRepository
{
    public BorrowRecordRepository(LibraryDBContext libraryDbContext): base(libraryDbContext){}

    public async Task<BorrowRecord?> GetBorrowRecordByBookIdAsync(Guid bookId, CancellationToken token)
    {
        return await _libraryDbContext.BorrowRecords
            .FirstOrDefaultAsync(br => br.bookId == bookId, token);
    }
}