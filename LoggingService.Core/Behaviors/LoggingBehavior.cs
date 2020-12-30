using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LoggingService.Core.Core.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> logger;

        public LoggingBehavior(ILogger<TRequest> logger)
        {
            this.logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                this.logger.LogInformation($"Logging Service application [Request Name: [{request}]");
                var response = await next();
                this.logger.LogInformation("Called handler with result {0}", response);
                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"Error occured in Logging Service application [Request:{request}], exception Message: [{ex.Message}]");

                return default(TResponse);
            }
        }
    }
}