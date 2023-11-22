using Microsoft.AspNetCore.Mvc;

namespace Wealth_Eco_Invest.Controllers
{
    using Services.Data.Interfaces;

    public class AnnounceController : Controller
    {
        private readonly IAnnounceService announceService;

        public AnnounceController(IAnnounceService announceService)
        {
            this.announceService = announceService;
        }
        public async Task<IActionResult> All()
        {
            var elements = await this.announceService.AllAnnounces();
            return View(elements);
        }
    }
}
