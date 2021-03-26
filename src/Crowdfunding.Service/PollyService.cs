using Microsoft.Extensions.Logging;

using Polly;
using Polly.Retry;

using System;

using Crowdfunding.Domain;

namespace Crowdfunding.Service
{
    public class PollyService : IPollyService
    {
        private GloballAppConfig _globallAppConfig;
        private readonly ILogger<PollyService> _logger;

        public PollyService(GloballAppConfig globallAppConfig, ILogger<PollyService> logger)
        {
            _globallAppConfig = globallAppConfig;
            _logger = logger;
        }

        public AsyncRetryPolicy CreatePoliticWaitAndRetryFor(string method)
        {
            var policy = Policy.Handle<Exception>().WaitAndRetryAsync(_globallAppConfig.Polly.QualityRetry, 
                attempt => TimeSpan.FromSeconds(_globallAppConfig.Polly.WaitingTimeSeconds), 
                (exception, calculatedWaitDuration) => {
                    _logger.LogError($"Error adding method {method}. Total retry time to date: {calculatedWaitDuration}s");
                });
            return policy;
        }
    }
}