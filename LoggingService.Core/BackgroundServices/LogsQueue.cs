using LoggingService.Core.Interfaces;
using LoggingService.Core.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoggingService.Core.BackgroundServices
{
    public class LogsQueue : ILogsQueue
    {
        private ConcurrentQueue<IEnumerable<LogItem>> _workItems =
         new ConcurrentQueue<IEnumerable<LogItem>>();
        private SemaphoreSlim _signal = new SemaphoreSlim(0);

        public Task QueueLogItems(
            IEnumerable<LogItem> workItem)
        {
            if (workItem == null)
            {
                throw new ArgumentNullException(nameof(workItem));
            }

            _workItems.Enqueue(workItem);
            _signal.Release();

            return Task.CompletedTask;
        }

        public async Task<IEnumerable<LogItem>> DequeueAsync(
            CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);
            _workItems.TryDequeue(out var workItem);

            return workItem;
        }

    }
}
