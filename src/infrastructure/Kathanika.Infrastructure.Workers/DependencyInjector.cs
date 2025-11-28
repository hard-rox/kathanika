using Kathanika.Infrastructure.Workers.Jobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Kathanika.Infrastructure.Workers;

public static class DependencyInjector
{
    public static IServiceCollection AddWorkers(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddQuartz(configure =>
        {
            //TODO: Get intervals from appSettings.
            JobKey processOutboxMessageJobKey = new(nameof(ProcessOutboxMessagesJob));
            configure.AddJob<ProcessOutboxMessagesJob>(opt => opt.WithIdentity(processOutboxMessageJobKey))
                .AddTrigger(trigger => trigger.ForJob(processOutboxMessageJobKey)
                    .WithSimpleSchedule(schedule => schedule.WithIntervalInMinutes(10)
                        .RepeatForever()));

            JobKey unusedUploadedFileCleanupJobKey = new(nameof(UnusedUploadedFileCleanupJob));
            configure.AddJob<UnusedUploadedFileCleanupJob>(opt => opt.WithIdentity(unusedUploadedFileCleanupJobKey))
                .AddTrigger(trigger => trigger.ForJob(unusedUploadedFileCleanupJobKey)
                    .WithSimpleSchedule(schedule => schedule.WithIntervalInMinutes(10)
                        .RepeatForever()));
        });

        services.AddQuartzHostedService();

        return services;
    }
}