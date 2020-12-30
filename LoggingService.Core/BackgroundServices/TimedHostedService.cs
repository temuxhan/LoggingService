using LoggingService.Core.Interfaces;
using LoggingService.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LoggingService.Core.BackgroundServices
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogsQueue logsQueue;
        private Timer _timer;

        public TimedHostedService(IServiceProvider serviceProvider, ILogsQueue logsQueue)
        {
            this.serviceProvider = serviceProvider;
            this.logsQueue = logsQueue;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                // Time span may come from some config
                TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var items = logsQueue.DequeueAsync(CancellationToken.None).Result;

            if (items != null)
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var loggingService = scope.ServiceProvider.GetRequiredService<ILoggingService>();
                    loggingService.AddLogItems(items);
                }
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}