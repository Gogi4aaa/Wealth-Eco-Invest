using Microsoft.AspNetCore.Mvc;

namespace Wealth_Eco_Invest.Controllers
{
	using Services.Data.Interfaces;
	using Web.Infrastructure.Extensions;

	public class ForumController : Controller
	{
		private readonly IForumService forumService;

		public ForumController(IForumService forumService)
		{
			this.forumService = forumService;
		}
		public async Task<IActionResult> All()
		{
			var all = await this.forumService.GetAllForumsByUserIdAsync(Guid.Parse(this.User.GetId()));
			//copy and paste code for latest message and others from /chat/all from controller

			return View();
		}
	}
}