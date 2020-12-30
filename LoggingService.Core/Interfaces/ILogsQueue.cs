using LoggingService.Core.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LoggingService.Core.Interfaces
{
    public interface ILogsQueue
    {
        Task QueueLogItems(IEnumerable<LogItem> workItem);

        Task<IEnumerable<LogItem>> DequeueAsync(CancellationToken cancellationToken);
    }
}