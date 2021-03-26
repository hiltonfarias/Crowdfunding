using Microsoft.AspNetCore.Mvc;

using NToastNotify;

using System.Threading.Tasks;

using Crowdfunding.Domain;
using Crowdfunding.Domain.ViewModels;

namespace Crowdfunding.MVC.Controllers
{
    public class DonationController : BaseController
    {
        private readonly IDonationService _donationService;
        private readonly IDomainNotificationService _domainNotificationService;

        public DonationController(
            IDonationService donationService, 
            IDomainNotificationService domainNotificationService,
            IToastNotification toastNotification
        ) : base(domainNotificationService,  toastNotification) 
        {
            _donationService = donationService;
            _domainNotificationService = domainNotificationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(nameof(Index), await _donationService.RecoverDonorsAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DonationViewModel donationViewModel)
        {
            _donationService.MakeDonationAsync(donationViewModel);

            if (_domainNotificationService.HasErrors)
            {
                AddDomainErrors();
                return View(donationViewModel);
            }

            AddNotificationOfSuccessfulTransaction("Donation successful!<p>Thank you for supporting our cause :)</p>");
            return RedirectToAction("Index", "Home");
        }
    }
}