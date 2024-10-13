using Library.Application.Interfaces;
using Library.Domain;

namespace Library.Persistance.DbPatterns.Repositories;

public class BorrowRecordRepository: IBorrowRecordRepository
{
    private readonly ILibraryDBContext _libraryDbContext;

    public BorrowRecordRepository(ILibraryDBContext libraryDbContext)
    {
        _libraryDbContext = libraryDbContext;
    }
    
    public async Task AddRecordAsync(BorrowRecord record, CancellationToken token)
    {
        await _libraryDbContext.BorrowRecords.AddAsync(record, token);
    }
}