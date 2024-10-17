using System.Security.Claims;
using Library.Persistance.Jobs;
using Microsoft.AspNetCore.SignalR;
using Quartz;

namespace Library.WebAPI.Config;

public static class QuartzConfig
{
    public static string UserId;
    
    public static IServiceCollection AddQuartz(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();
            
            var jobKey = new JobKey("CheckExpiringBorrowRecordsJob");

            var jobDataMap = new JobDataMap
            {
                { "UserId", UserId }
            };

            q.AddJob<ExpiringBorrowRecordJob>(opts => opts
                .WithIdentity(jobKey)
                .UsingJobData(jobDataMap));
            
            q.ScheduleJob<ExpiringBorrowRecordJob>(trigger => trigger
                .WithIdentity("CheckExpiringBorrowRecordsJob-trigger")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInHours(24)
                    .RepeatForever()));
        });
        
        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        
        return services;
    }
}