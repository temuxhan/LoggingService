using LoggingService.Core.Entities;
using System.Collections.Generic;

namespace LoggingService.Core.Models
{
    public class Application : BaseEntity
    {
        public string Name { get; set; }

        public List<LogMessage> LogMessages { get; set; }
    }
}