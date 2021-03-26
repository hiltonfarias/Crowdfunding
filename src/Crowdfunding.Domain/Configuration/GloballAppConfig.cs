using System;

namespace Crowdfunding.Domain
{
    public class GloballAppConfig
    {
        public int CampaignObjective { get; set; }
        public DateTime CampaignEndDate { get; set; }
        public RetryPolicy Polly { get; set; }
    }

    public class RetryPolicy
    {
        public int QualityRetry { get; set; }
        public int WaitingTimeSeconds { get; set; }
    }
}