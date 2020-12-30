using LoggingService.Core.Models;
using LoggingService.Core.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggingService
{
    public class LoggingService : ILoggingService
    {
        private readonly DbContextOptions<LoggingDbContext> options;

        public LoggingService(DbContextOptions<LoggingDbContext> options)
        {
            this.options = options;
        }

        public async Task AddLogItems(IEnumerable<LogItem> li)
        {
            using (LoggingDbContext context = new LoggingDbContext(this.options))
            {
                foreach (var logItem in li)
                {
                    var application = context.Application.FirstOrDefault(app => app.Name == logItem.Application);

                    if (application == null)
                    {
                        context.Add(application = new Application { Name = logItem.Application });
                        await context.SaveChangesAsync();
                    }

                    await context.AddAsync(new LogMessage { ApplicationId = application.Id, Log_level = logItem.LogLevel, Date = logItem.Date, Message = logItem.Message });
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
