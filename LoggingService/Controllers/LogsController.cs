using LoggingService.Core;
using LoggingService.Core.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace LoggingService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class LogsController : ControllerBase
    {
        private readonly IMediator mediator;

        public LogsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<string> Log(
            [FromBody]
            [Required]
            IEnumerable<LogMessageModel> logMessageModels)
        {
            var response = await this.mediator.Send(new LoggingRequestCommand { LogMessages = logMessageModels });

            return response.ResposneMessage;
        }
    }
}