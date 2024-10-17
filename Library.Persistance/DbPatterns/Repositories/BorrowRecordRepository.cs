using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Application.BorrowRecords.Queries.GetExpiringBorrowRecord;
using Library.Application.Interfaces;
using Library.Application.Interfaces.Repositories;
using Library.Domain;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistance.DbPatterns.Repositories;

public class BorrowRecordRepository: BaseRepository<BorrowRecord>, IBorrowRecordRepository
{
    private readonly IMapper _mapper;

    public BorrowRecordRepository(LibraryDBContext libraryDbContext, IMapper mapper) : base(libraryDbContext)
    {
        _mapper = mapper;
    }

    public async Task<BorrowRecord?> GetBorrowRecordByBookIdAsync(Guid bookId, CancellationToken token)
    {
        return await _libraryDbContext.BorrowRecords
            .FirstOrDefaultAsync(br => br.bookId == bookId, token);
    }

    public async Task<ExpiringRecordVm> GetExpiringRecordsAsync(string userId, CancellationToken token)
    {
        var currentDate = DateTime.UtcNow;

        var recordList = await _libraryDbContext.books
            .Include(book => book.borrowRecords)
            .Where(book => book.borrowRecords.Any(record =>
                record.userId == userId &&
                record.book_issue_expiration_date > currentDate && 
                record.book_issue_expiration_date <= currentDate.AddDays(30)))
            .ProjectTo<ExpiringBookDto>(_mapper.ConfigurationProvider)
            .ToListAsync(token);

        return new ExpiringRecordVm { records = recordList };
    }
}