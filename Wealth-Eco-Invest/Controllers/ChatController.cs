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
				chat.Message = await this.messageService.GetLatestMessage(chat.ChatId, chat.UserFrom, chat.UserTo);
				chat.Name = await this.messageService.GetLatestMessageOwner(chat.ChatId, chat.UserFrom, chat.UserTo);
				if (chat.Message == "" || chat.Name == "")
				{
					chat.Name = await this.userService.GetUserNameByIdAsync(chat.UserTo);
					chat.Message = "Все още нямате съобщения с този потребител!";
				}
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

		[HttpGet]
		public async Task<IActionResult> MethodCall(string message, string chatId)
		{

			//await this.chatHubContext.Clients.All.SendAsync("ReceiveMessage", this.User.Identity.Name, message);
			string username = this.User.Identity.Name;
			await this.messageService.SaveMessageAsync(message, Guid.Parse(chatId), username);
			return RedirectToAction("Chat", "Chat", new {chatId = Guid.Parse(chatId)});
		}
	}
}
