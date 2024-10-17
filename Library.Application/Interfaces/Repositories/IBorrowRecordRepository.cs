﻿using Library.Application.BorrowRecords.Queries.GetExpiringBorrowRecord;
using Library.Domain;

namespace Library.Application.Interfaces.Repositories;

public interface IBorrowRecordRepository: IBaseRepository<BorrowRecord>
{
    Task<BorrowRecord?> GetBorrowRecordByBookIdAsync(Guid bookId, CancellationToken token);
    Task<ExpiringRecordVm> GetExpiringRecordsAsync(string userId, CancellationToken token);
}