namespace Wealth_Eco_Invest.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Services.Data.Interfaces;
	using Web.Infrastructure.Extensions;
	using Web.ViewModels.Messages;

	public class MessagesController : Controller
	{
		private readonly IMessageService messageService;
		private readonly IUserService userService;
		public MessagesController(IMessageService messageService, IUserService userService)
		{
			this.messageService = messageService;
			this.userService = userService;
		}

		[HttpGet]
		public async Task<IActionResult> All()
		{
			var all = await this.messageService.GetAllChatsByUserId(Guid.Parse(this.User.GetId()!));
			foreach (var chat in all)
			{
				chat.Name = await this.userService.GetUserNameByIdAsync(chat.UserTo);
			}

			return View(all);
		}

		public async Task<JsonResult> GetChat(Guid chatId)
		{
			return Json(new MessageViewModel());
		}
	}
}
