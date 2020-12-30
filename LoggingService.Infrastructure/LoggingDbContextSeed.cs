using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LoggingService
{
    public class LoggingDbContextSeed
    {
        public static Task SeedAsync(LoggingDbContext loggingDbContext,
            ILoggerFactory loggerFactory, int? retry = 0)
        {
            return Task.CompletedTask;
        }
    }
}