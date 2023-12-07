using Microsoft.AspNetCore.Mvc;

namespace Wealth_Eco_Invest.Controllers
{
	using System.Security.Claims;
	using Common;
	using Data.Models;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Identity.UI.Services;
	using Microsoft.AspNetCore.Mvc.Infrastructure;
	using Services.Data.Interfaces;
    using Services.Data.Models;
	using Services.Messaging.Templates;
	using Web.Infrastructure.Extensions;
	using Web.ViewModels.Announce;
	using IEmailSender = Services.Messaging.IEmailSender;
	using static Common.EmailSendTemplate;
	using static Common.NotificationMessagesConstants;
	[Authorize]
	public class AnnounceController : Controller
    {
        private readonly IAnnounceService announceService;
        private readonly ICategoryService categoryService;
		private readonly IEmailSender emailSender;
        public AnnounceController(IAnnounceService announceService, ICategoryService categoryService,IEmailSender emailSender)
        {
            this.announceService = announceService;
            this.categoryService = categoryService;
			this.emailSender = emailSender;
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
	        try
	        {
				await this.announceService.AddAnnounceAsync(formModel);
				TempData[SuccessMessage] = "You successfully added your announce!";
	        }
	        catch (Exception e)
	        {
		        TempData[ErrorMessage] = "Unexpected message occurred";
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
		public async Task<IActionResult> Delete(Guid id)
		{
			try
			{
				await this.announceService.DeleteAnnounceByIdAsync(id);

				TempData[SuccessMessage] = "You successfully delete your announce!";
			}
			catch (Exception)
			{
				TempData[ErrorMessage] = "Unexpected message occurred";
			}
			
			return RedirectToAction("All", "Announce");
		}

		[HttpGet]
		public async Task<IActionResult> Edit(Guid id)
		{
			AnnounceFormModel announceForEdit = await this.announceService.GetAnnounceForEditAsync(id);
			announceForEdit.Categories = await this.categoryService.AllCategoriesAsync();
			return View(announceForEdit);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Guid id, AnnounceFormModel model)
		{
			if (!ModelState.IsValid)
			{
				model.Categories = await this.categoryService.AllCategoriesAsync();
				return View(model);
			}

			await this.announceService.EditAnnounceByIdAndFormModelAsync(id, model);

			TempData[SuccessMessage] = "Your announce was updated!";

			return RedirectToAction("All", "Announce");
		}

		[HttpGet]
		public async Task<IActionResult> Buy()
		{
			var callbackUrl = Url.Page(
				"/",
				pageHandler: null,
				values: new { area = ""},
				protocol: Request.Scheme);

			await this.emailSender.SendEmailAsync(this.User.GetEmail()!, "Announce buying", AnnounceBuyingTemplate.Message(this.User.Identity!.Name!, callbackUrl));

			return RedirectToAction("All", "Announce");
		}
	}
}
