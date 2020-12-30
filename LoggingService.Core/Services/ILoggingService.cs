using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoggingService.Core.Services
{
    public interface ILoggingService
    {
        Task AddLogItems(IEnumerable<LogItem> li);
    }
}