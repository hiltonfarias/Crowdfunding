using Microsoft.AspNetCore.Mvc;

using NToastNotify;

using Crowdfunding.Domain;

namespace Crowdfunding.MVC.Controllers
{
    public class BaseController : Controller
    {
        private readonly IDomainNotificationService _domainNotificationService;
        private readonly IToastNotification _toastNotification;

        public BaseController(
            IDomainNotificationService domainNotificationService, 
            IToastNotification toastNotification)
        {
            _domainNotificationService = domainNotificationService;
            _toastNotification = toastNotification;
        }

        protected void AddNotificationOfSuccessfulTransaction(string successMessage = null)
        {
            _toastNotification.AddSuccessToastMessage(successMessage ?? "Operation performed successfully!");
        }
        protected void AddDomainErrors()
        {
            var errorsMessage = _domainNotificationService.HasErrors 
                ? _domainNotificationService.RecoverDomainErrorsFormattedHTML() : null;
            
            if (!string.IsNullOrEmpty(errorsMessage))
            {
                _toastNotification.AddErrorToastMessage(errorsMessage);
            }
        }
    }
}