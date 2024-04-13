namespace Wealth_Eco_Invest.Controllers
{
	using Data.Models;
	using Hubs;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.SignalR;
	using Services.Data.Interfaces;
	using Web.Infrastructure.Extensions;
	using Web.ViewModels.Messages;
	using static Common.NotificationMessagesConstants;
	using static Common.GeneralApplicationConstants;
	public class ChatController : Controller
	{
		private readonly IChatService chatService;
		private readonly IMessageService messageService;
		private readonly IUserService userService;
		private readonly IHubContext<ChatHub> chatHubContext;
		public ChatController(IMessageService messageService, IUserService userService, IChatService chatService, IHubContext<ChatHub> chatHubContext)
		{
			this.messageService = messageService;
			this.userService = userService;
			this.chatService = chatService;
			this.chatHubContext = chatHubContext;
		}

		[HttpGet]
		public async Task<IActionResult> All()
		{
			var all = await this.chatService.GetAllChatsByUserId(Guid.Parse(this.User.GetId()!));
			foreach (var chat in all)
			{
				chat.Name = await this.userService.GetUserNameByIdAsync(chat.UserTo);
			}

			return View(all);
		}

		[HttpGet]
		public async Task<IActionResult> Add(string ownerName, Guid announceId)
		{
			try
			{
				Guid userToId = await this.userService.GetUserIdByUsernameAsync(ownerName);
				Guid userFromId = Guid.Parse(this.User.GetId()!);
				await this.chatService.AddChatAsync(userFromId, userToId, announceId);
			}
			catch (Exception e)
			{
				TempData[ErrorMessage] = CommonErrorMessage;
			}

			return RedirectToAction("All", "Chat");
		}

		public async Task<IActionResult> Chat(Guid chatId)
		{
			var chat = await this.chatService.GetChatByChatIdAsync(chatId);
			chat.Messages = await this.messageService.GetAllMessagesByChatIdAsync(chatId);
			chat.MessageInput = "";
			return View(chat);
		}

		[HttpPost]
		public async Task<IActionResult> SaveData(string message, Guid chatId)
		{
			//sprqmo chata Id-to da vzema connectionId-to na UserTo 
			//await this.chatHubContext.Clients.Client().SendAsync("ReceiveMessage");
			await this.messageService.SaveMessageAsync(message, chatId);
			return RedirectToAction("Chat", "Chat", new {chatId = chatId});
		}
		[HttpGet]
		public async Task<IActionResult> MethodCall(string message, string chatId)
		{

			//await this.chatHubContext.Clients.All.SendAsync("ReceiveMessage", this.User.Identity.Name, message);
			await this.messageService.SaveMessageAsync(message, Guid.Parse(chatId));
			return RedirectToAction("Chat", "Chat", new {chatId = Guid.Parse(chatId)});
		}
	}
}
