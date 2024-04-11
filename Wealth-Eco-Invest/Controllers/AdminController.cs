namespace Wealth_Eco_Invest.Controllers
{
	using Wealth_Eco_Invest.Services.Data.Interfaces;

	using Web.ViewModels.Admin;

	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using Services.Models.Admin;
	using static Common.GeneralApplicationConstants;
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
		public async Task<IActionResult> All(int page = 1)
		{
			AllUsersViewModel usersViewModel = await this.adminService.GetAllUsers();
			var model = new AdminServiceModel()
			{
				Users = usersViewModel.Users
					.Skip((page - 1) * UsersPerPage)
					.Take(UsersPerPage).ToArray(),
				CurrentPage = page,
				TotalUsers = usersViewModel.Users.Count,
				UsersPerPage = UsersPerPage
			};
			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> UserAnnounces(Guid userId)
		{
			var allAnnounces = await this.adminService.GetAllAnnouncesByUserIdAsync(userId);

			return View(allAnnounces);
		}
	}
}
