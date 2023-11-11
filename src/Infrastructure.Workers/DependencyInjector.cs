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
            JobKey jobKey = new(nameof(ProcessOutboxMessagesJob));

            // configure.AddJob<ProcessOutboxMessagesJob>(jobKey)
            //     .AddTrigger(trigger => trigger.ForJob(jobKey)
            //         .WithSimpleSchedule(schedule => schedule.WithIntervalInSeconds(10)
            //             .RepeatForever()));                        
        });

        services.AddQuartzHostedService();

        return services;
    }
}
