using Microsoft.AspNetCore.Mvc;

namespace Wealth_Eco_Invest.Controllers
{
    using Services.Data.Interfaces;
    using Services.Data.Models;
    using Web.ViewModels.Announce;


	public class AnnounceController : Controller
    {
        private readonly IAnnounceService announceService;
        private readonly ICategoryService categoryService;
        public AnnounceController(IAnnounceService announceService, ICategoryService categoryService)
        {
            this.announceService = announceService;
            this.categoryService = categoryService;
        }
        public async Task<IActionResult> All([FromQuery] AnnounceQueryViewModel queryViewModel)
        {
            AllAnnouncesFilteredAndPagedServiceModel serviceModel = await this.announceService.GetAllAnnounces(queryViewModel);

            queryViewModel.Announces = serviceModel.Announces;
            queryViewModel.TotalAnnounces = serviceModel.TotalAnnouncesCount;
            queryViewModel.Categories = await this.categoryService.AllCategoriesNamesAsync();

            return View(queryViewModel);
        }
    }
}
