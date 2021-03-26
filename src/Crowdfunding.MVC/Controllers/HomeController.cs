using System.Threading.Tasks;

using NToastNotify;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Crowdfunding.Domain;

namespace Crowdfunding.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeInfoService _homeInfoService;
        private readonly ILogger<HomeController> _logger;
        private readonly IToastNotification _toastNotification;

        public HomeController(
            IHomeInfoService homeInfoService, 
            ILogger<HomeController> logger, 
            IToastNotification toastNotification
        )
        {
            _homeInfoService = homeInfoService;
            _logger = logger;
            _toastNotification = toastNotification;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _homeInfoService.RecoverDataInitialHomeAsync());
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
