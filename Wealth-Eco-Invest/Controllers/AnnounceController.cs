using Microsoft.AspNetCore.Mvc;
namespace Wealth_Eco_Invest.Controllers
{
	using Data.Models;
    using Microsoft.AspNetCore.Authorization;
	using Services.Data.Interfaces;
    using Services.Data.Models;
    using Services.Messaging.Templates;
    using Web.Infrastructure.Extensions;
    using Web.ViewModels.Announce;
    using Web.ViewModels.Announce.Enums;
    using IEmailSender = Services.Messaging.IEmailSender;
    using static Common.NotificationMessagesConstants;
    using Wealth_Eco_Invest.Services.Data.Models.Announces;
	using static Common.GeneralApplicationConstants;

    [Authorize(Roles = "User")]
	public class AnnounceController : Controller
    {
        private readonly IAnnounceService announceService;
        private readonly ICategoryService categoryService;
		private readonly IEmailSender emailSender;
		private readonly IAdminService adminService;
		public AnnounceController(IAnnounceService announceService, ICategoryService categoryService,IEmailSender emailSender, IAdminService adminService)
        {
            this.announceService = announceService;
            this.categoryService = categoryService;
			this.emailSender = emailSender;
			this.adminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> All([FromQuery] AnnounceQueryViewModel queryViewModel, int orderBy)
        {

	        if (orderBy == 1)
	        {
				queryViewModel.AnnounceSorting = AnnounceSorting.Oldest;
			}
			else if (orderBy == 2)
	        {
		        queryViewModel.AnnounceSorting = AnnounceSorting.PriceAscending;
			}
			else if (orderBy == 3)
	        {
		        queryViewModel.AnnounceSorting = AnnounceSorting.PriceDescending;
			}

	        try
	        {
				AllAnnouncesFilteredAndPagedServiceModel serviceModel = await this.announceService.GetAllAnnouncesAsync(queryViewModel);
				
				ViewData["announceSorting"] = (int)queryViewModel.AnnounceSorting;
				
				queryViewModel.Announces = serviceModel.Announces;
				queryViewModel.TotalAnnounces = serviceModel.TotalAnnouncesCount;
				queryViewModel.Categories = await this.categoryService.AllCategoriesNamesAsync();
	        }
	        catch (Exception)
	        {
				TempData[ErrorMessage] = "Unexpected error occurred";
			}
	        
            return this.View(queryViewModel);
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
	        try
	        {
				await this.announceService.AddAnnounceAsync(formModel);
				TempData[SuccessMessage] = "You successfully added your announce!";
	        }
	        catch (Exception e)
	        {
		        TempData[ErrorMessage] = "Unexpected error occurred";
	        }
			
	        return RedirectToAction("All", "Announce");
        }
        [HttpGet]
		public async Task<IActionResult> Details(Guid id)
		{
			AnnounceDetailsViewModel announce = await this.announceService.GetAnnounceForDetailsByIdAsync(id);

            return View(announce);
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Delete(Guid id)
		{
			try
			{
				await this.announceService.DeleteAnnounceByIdAsync(id);

				TempData[SuccessMessage] = "You successfully delete your announce!";
			}
			catch (Exception)
			{
				TempData[ErrorMessage] = "Unexpected error occurred";
			}

			if (await this.adminService.IsUserAdmin(Guid.Parse(this.User.GetId()!)))
			{
				return RedirectToAction("All", "Admin");
			}

			return RedirectToAction("All", "Announce");
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Edit(Guid id)
		{
			AnnounceFormModel announceForEdit = await this.announceService.GetAnnounceForEditAsync(id);
			announceForEdit.Categories = await this.categoryService.AllCategoriesAsync();
            return View(announceForEdit);

        }

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Edit(Guid id, AnnounceFormModel model)
		{
			if (!ModelState.IsValid)
			{
				model.Categories = await this.categoryService.AllCategoriesAsync();
				return View(model);
			}

			try
			{
				await this.announceService.EditAnnounceByIdAndFormModelAsync(id, model);

				TempData[SuccessMessage] = "Your announce was updated!";
			}
			catch (Exception)
			{
				TempData[ErrorMessage] = "Unexpected error occurred";
			}

			if (await this.adminService.IsUserAdmin(Guid.Parse(this.User.GetId()!)))
			{
				return RedirectToAction("All", "Admin");
			}

			return RedirectToAction("All", "Announce");
		}

    }
}
