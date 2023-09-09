using Quartz;

namespace Kathanika.Infrastructure.Workers.Jobs;

internal sealed class ProcessOutboxMessagesJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("Hello from JOB");
    }
}