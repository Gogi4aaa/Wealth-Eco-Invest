using Wealth_Eco_Invest.Services.Data.Interfaces;
using Wealth_Eco_Invest.Web.ViewModels.Admin;

namespace Wealth_Eco_Invest.Controllers
{
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[Authorize(Roles = "Administrator")]
	public class AdminController : Controller
	{
		private readonly IAdminService adminService;
		private readonly IAnnounceService announceService;
		public AdminController(IAdminService adminService, IAnnounceService announceService)
		{
			this.adminService = adminService;
			this.announceService = announceService;
		}

		[HttpGet]
		public async Task<IActionResult> All(Guid userId)
		{
			AllAnnouncesForEachUser announces = await this.adminService.GetAllAnnouncesForEachUser();

			return View(announces);
		}

		[HttpGet]
		public async Task<IActionResult> UserAnnounces(Guid userId)
		{
			var allAnnounces = await this.adminService.GetAllAnnouncesByUserIdAsync(userId);

			return View(allAnnounces);
		}
	}
}
