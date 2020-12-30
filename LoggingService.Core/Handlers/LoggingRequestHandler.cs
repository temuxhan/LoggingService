using LoggingService.Core.Commands;
using LoggingService.Core.Interfaces;
using LoggingService.Core.Services;
using LoggingService.SharedKernel;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LoggingService.Core.Handlers
{
    public class LoggingRequestHandler : IRequestHandler<LoggingRequestCommand, LogMessageResponse>
    {
        private readonly ILoggingService loggingService;
        private readonly ILogsQueue logsQueue;

        public LoggingRequestHandler(ILoggingService loggingService, ILogsQueue logsQueue)
        {
            this.loggingService = loggingService;
            this.logsQueue = logsQueue;
        }

        public Task<LogMessageResponse> Handle(LoggingRequestCommand request, CancellationToken cancellationToken)
        {
            var messages = request.LogMessages;
            var items = messages.Select(message =>
            {
                return new LogItem
                {
                    LogLevel = LogLevelParser.GetLogLevelFromMessage(message.Message),
                    Message = LogLevelParser.GetMessageWithoutLogLevel(message.Message),
                    Date = TimeSpan.FromTicks(message.Log_date),
                    Application = message.Application
                };
            });

            logsQueue.QueueLogItems(items);

            return Task.FromResult(new LogMessageResponse("Logs were succesfully queued"));
        }
    }
}