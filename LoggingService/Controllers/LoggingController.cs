using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggingService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoggingController : ControllerBase
    {
        private readonly ILogger<LoggingController> _logger;

        public LoggingController(ILogger<LoggingController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public void Log([FromBody] IEnumerable<LogMessageModel> logMessageModels )
        {
        }

        public class LogMessageModel
        {
            public long log_date { get; set; }
            public string application { get; set; }

            public string message { get; set; }
        }
    }
}
