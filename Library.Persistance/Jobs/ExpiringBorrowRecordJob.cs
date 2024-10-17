using Library.Application.Interfaces;
using MediatR;
using Quartz;

namespace Library.Persistance.Jobs;

public class ExpiringBorrowRecordJob: BaseJob, IJob
{
    public ExpiringBorrowRecordJob(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    
    public async Task Execute(IJobExecutionContext context)
    {
        var userId = context.MergedJobDataMap.GetString("UserId");
        if(userId == null) return;
        var books = await _unitOfWork.BorrowRecords.GetExpiringRecordsAsync(userId, new CancellationToken());
    }
}