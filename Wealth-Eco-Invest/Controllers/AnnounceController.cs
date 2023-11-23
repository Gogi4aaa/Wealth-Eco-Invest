using Microsoft.AspNetCore.Mvc;

namespace Wealth_Eco_Invest.Controllers
{
	using Microsoft.AspNetCore.Authorization;
	using Services.Data.Interfaces;
    using Services.Data.Models;
	using Web.Infrastructure.Extensions;
	using Web.ViewModels.Announce;

    [Authorize]
	public class AnnounceController : Controller
    {
        private readonly IAnnounceService announceService;
        private readonly ICategoryService categoryService;
        public AnnounceController(IAnnounceService announceService, ICategoryService categoryService)
        {
            this.announceService = announceService;
            this.categoryService = categoryService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AnnounceQueryViewModel queryViewModel)
        {
	        AllAnnouncesFilteredAndPagedServiceModel serviceModel = await this.announceService.GetAllAnnouncesAsync(queryViewModel);

            queryViewModel.Announces = serviceModel.Announces;
            queryViewModel.TotalAnnounces = serviceModel.TotalAnnouncesCount;
            queryViewModel.Categories = await this.categoryService.AllCategoriesNamesAsync();

            return View(queryViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AnnounceFormModel formModel = new AnnounceFormModel();
            formModel.Categories = await this.categoryService.AllCategoriesAsync();
            return View(formModel);
        }

        [HttpPost]
		public async Task<IActionResult> Add(AnnounceFormModel formModel)
        {
	        if (!ModelState.IsValid)
	        {
		        formModel.Categories = await this.categoryService.AllCategoriesAsync();
				return View(formModel);
	        }
	        formModel.UserId = Guid.Parse(this.User.GetId()!);
			await this.announceService.AddAsync(formModel);
	        return RedirectToAction("All", "Announce");
        }
    }
}
