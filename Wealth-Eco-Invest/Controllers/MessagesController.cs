namespace Wealth_Eco_Invest.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Services.Data.Interfaces;
	using Web.Infrastructure.Extensions;
	using Web.ViewModels.Messages;

	public class MessagesController : Controller
	{
		private readonly IMessageService messageService;

		public MessagesController(IMessageService messageService)
		{
			this.messageService = messageService;
		}
		public async Task<IActionResult> All()
		{
			var all = await this.messageService.GetAllMessagesByUserAsync(Guid.Parse(this.User.GetId()!));
			return View();
		}
	}
}
