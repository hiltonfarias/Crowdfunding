using Polly.Retry;

namespace Crowdfunding.Service
{
    public interface IPollyService
    {
         AsyncRetryPolicy CreatePoliticWaitAndRetryFor(string method);
    }
}