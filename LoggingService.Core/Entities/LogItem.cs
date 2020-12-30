using LoggingService.Core.Entities;
using LoggingService.SharedKernel;
using System;

namespace LoggingService.Core.Services
{
    [Serializable]
    public class LogItem : BaseEntity
    {
        public TimeSpan Date { get; set; }
        public string Application { get; set; }

        public string Message { get; set; }

        public LogLevel LogLevel { get; set; }
    }
}