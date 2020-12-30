using MediatR;
using System.Collections.Generic;

namespace LoggingService.Core.Commands
{
    public class LoggingRequestCommand : IRequest<LogMessageResponse>
    {
        public IEnumerable<LogMessageModel> LogMessages { get; set; }
    }
}