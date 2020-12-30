using LoggingService.Core.Entities;
using LoggingService.SharedKernel;
using System;

namespace LoggingService.Core.Models
{
    public class LogMessage : BaseEntity
    {
        public string Message { get; set; }

        public TimeSpan Date { get; set; }

        public LogLevel Log_level { get; set; }

        public int ApplicationId { get; set; }

        public Application Application { get; set; }
    }
}