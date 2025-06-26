using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Common.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            _logger.LogInformation("Handling request: {RequestName} - {@Request}", requestName, request);

            try
            {
                var response = await next();
                _logger.LogInformation("Request: {RequestName} handled successfully - {@Response}", requestName, response);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Request: {RequestName} failed", requestName);
                throw;
            }
        }
    }
}
