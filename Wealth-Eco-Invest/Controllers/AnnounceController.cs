namespace Wealth_Eco_Invest.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Data.Models;
    using Microsoft.AspNetCore.Authorization;
	using Services.Data.Interfaces;
	using Services.Messaging.Templates;
	using Services.Models.Announce;
	using Web.Infrastructure.Extensions;
    using Web.ViewModels.Announce;
    using Web.ViewModels.Announce.Enums;
    using IEmailSender = Services.Messaging.IEmailSender;
    using static Common.NotificationMessagesConstants;
	using Web.Infrastructure.Files;
	using Web.Infrastructure.ImagesCloud;
	using static Common.GeneralApplicationConstants;

    [Authorize(Roles = "User")]
	public class AnnounceController : Controller
    {
        private readonly IAnnounceService announceService;
        private readonly ICategoryService categoryService;
		private readonly IEmailSender emailSender;
		private readonly IAdminService adminService;
		private readonly IChatService chatService;
		private readonly CloudinarySetUp cloudinarySetUp;
		public AnnounceController(IAnnounceService announceService, ICategoryService categoryService,IEmailSender emailSender, IAdminService adminService, IChatService chatService)
        {
            this.announceService = announceService;
            this.categoryService = categoryService;
			this.emailSender = emailSender;
			this.adminService = adminService;
			this.chatService = chatService;
			this.cloudinarySetUp = new CloudinarySetUp();
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
		public async Task<IActionResult> Add(AnnounceFormModel model)
        {
	        ModelState.Remove(nameof(model.ImageUrl));
	        if (!ModelState.IsValid)
	        {
		        model.Categories = await this.categoryService.AllCategoriesAsync();
				return View(model);
	        }
	        string fullPath = String.Empty;
	        bool isFileCreated = false;
	        try
	        {
		        model.UserId = Guid.Parse(this.User.GetId()!);
		        string fileName = model.ProductImage.FileName;
		        model.ImageUrl = fileName;
		        CreateFile.CreateImageFile(model);
		        isFileCreated = true;
		        fullPath = Path.GetFullPath(fileName);
		        await cloudinarySetUp.UploadAsync(fullPath);
		        var correctImageUrl = cloudinarySetUp.GenerateImageUrl(fileName);
		        model.ImageUrl = correctImageUrl;

				var announceId = await this.announceService.AddAnnounceAsync(model);

				await this.chatService.AddChatAsync(Guid.Parse(this.User.GetId()), null, announceId);
			}
	        catch (Exception e)
	        {
		        TempData[ErrorMessage] = CommonErrorMessage;
		        return View(model);
	        }
	        finally
	        {
		        if (isFileCreated)
		        {
			        System.IO.File.Delete(fullPath);
		        }
	        }
	        TempData[SuccessMessage] = "Успешно добавихте нов продукт!";
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
			ModelState.Remove(nameof(model.ImageUrl));
			ModelState.Remove(nameof(model.ProductImage));
			if (!ModelState.IsValid)
			{
				model.Categories = await this.categoryService.AllCategoriesAsync();
				return View(model);
			}
			model.UserId = Guid.Parse(this.User.GetId()!);
			string fullPath = String.Empty;
			bool isFileCreated = false;
			try
			{
				if (model.ProductImage == null)
				{
					var item = await this.announceService.GetAnnounceForDetailsByIdAsync(id);
					model.ImageUrl = item.ImageUrl;
				}
				else
				{
					string fileName = model.ProductImage.FileName;
					model.ImageUrl = fileName;
					CreateFile.CreateImageFile(model);
					isFileCreated = true;
					fullPath = Path.GetFullPath(fileName);
					await cloudinarySetUp.UploadAsync(fullPath);
					var correctImageUrl = cloudinarySetUp.GenerateImageUrl(fileName);
					model.ImageUrl = correctImageUrl;
				}

				await this.announceService.EditAnnounceByIdAndFormModelAsync(id, model);

				TempData[SuccessMessage] = "Вашият продукт е успешно редактиран!";
			}
			catch (Exception e)
			{
				TempData[ErrorMessage] = CommonErrorMessage;
			}
			finally
			{
				if (isFileCreated)
				{
					System.IO.File.Delete(fullPath);
				}
			}

			if (this.User.IsInRole(AdminRoleName))
			{
				return RedirectToAction("All", "Admin");
			}

			return RedirectToAction("All", "Announce");
		}

    }
}
