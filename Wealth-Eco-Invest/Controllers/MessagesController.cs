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
		public async Task<IActionResult> All()
		{
			var all = await this.messageService.GetAllMessagesByUserAsync(Guid.Parse(this.User.GetId()!));
			foreach (var message in all)
			{
				message.Name = await this.userService.GetUserNameByIdAsync(message.UserTo);
			}
			return View();
		}
	}
}
