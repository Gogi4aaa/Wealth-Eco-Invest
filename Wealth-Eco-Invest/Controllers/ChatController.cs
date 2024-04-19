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
	using static Wealth_Eco_Invest.Common.ValidationConstants;
	using Stripe;
	using NuGet.Packaging.Signing;
	using System.Text.RegularExpressions;

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
		public async Task<IActionResult> All(string clickedChatId = "")
		{
			var allChats = await this.chatService.GetAllChatsByUserId(Guid.Parse(this.User.GetId()!));
			foreach (var chat in allChats)
			{
				chat.Message = await this.messageService.GetLatestMessage(chat.ChatId, chat.UserFrom, chat.UserTo);
				chat.Name = await this.messageService.GetLatestMessageOwner(chat.ChatId, chat.UserFrom, chat.UserTo);

				if (chat.Name == this.User.Identity.Name)
				{
					chat.Name = "You: ";
				}
				else
				{
					chat.Name += ": ";
				}
				if (chat.Message == "" || chat.Name == "")
				{
					chat.Name = "";
					chat.Message = "Все още нямате съобщения с този потребител!";
				}

				
			}

			var chatId = new Guid();
			if (clickedChatId == "")
			{
				chatId = await this.chatService.GetLatestChatIdAsync(Guid.Parse(this.User.GetId()));
			}
			else
			{
				chatId = Guid.Parse(clickedChatId);
			}
			 
			var allMessages = await this.messageService.GetAllMessagesByChatIdAsync(chatId);
			foreach (var messageViewModel in allMessages)
			{
				messageViewModel.UserFrom = await this.userService.GetUserIdByUsernameAsync(messageViewModel.Owner);
				var userChatFrom = await this.userService.GetUserNameByIdAsync(messageViewModel.ChatUserFrom);
				var userChatTo = await this.userService.GetUserNameByIdAsync(messageViewModel.ChatUserTo);
				if (messageViewModel.Owner != userChatFrom)
				{
					messageViewModel.UserTo = await this.userService.GetUserIdByUsernameAsync(userChatFrom);
				}
				else if (messageViewModel.Owner != userChatTo)
				{
					messageViewModel.UserTo = await this.userService.GetUserIdByUsernameAsync(userChatTo);
				}
			}
			var allChatsAndMessages = new AllChatsAndMessagesViewModel()
			{
				Chats = allChats,
				Messages = allMessages,
				ChatId = chatId
			};
			return View(allChatsAndMessages);
		}

		[HttpGet]
		public async Task<IActionResult> Add(string ownerName, Guid announceId)
		{
			try
			{
				Guid userToId = await this.userService.GetUserIdByUsernameAsync(ownerName);
				Guid userFromId = Guid.Parse(this.User.GetId()!);
				var isChatAlreadyExist = await this.chatService.IsChatAlreadyExist(userFromId, userToId);
				if (isChatAlreadyExist)
				{
					TempData[ErrorMessage] = "Вече имате чат с този потребител!";
					return RedirectToAction("All", "Chat");
				}
				
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
			var all = await this.messageService.GetAllMessagesByChatIdAsync(chatId);
			foreach (var messageViewModel in all)
			{
				messageViewModel.UserFrom = await this.userService.GetUserIdByUsernameAsync(messageViewModel.Owner);
				var userChatFrom = await this.userService.GetUserNameByIdAsync(messageViewModel.ChatUserFrom);
				var userChatTo = await this.userService.GetUserNameByIdAsync(messageViewModel.ChatUserTo);
				if (messageViewModel.Owner != userChatFrom)
				{
					messageViewModel.UserTo = await this.userService.GetUserIdByUsernameAsync(userChatFrom);
				}
				else if (messageViewModel.Owner != userChatTo)
				{
					messageViewModel.UserTo = await this.userService.GetUserIdByUsernameAsync(userChatTo);
				}
			}

			chat.Messages = all;
			
			
			return View(chat);
		}

		[HttpGet]
		public async Task<IActionResult> MethodCall(string message, string chatId)
		{
			
			string username = this.User.Identity.Name;
			await this.messageService.SaveMessageAsync(message, Guid.Parse(chatId), username);
			
			return RedirectToAction("All", "Chat", new { clickedChatId = chatId});
		}
		[HttpGet]
		public async Task<IActionResult> SendMessage(string message, Guid chatId)
		{
			var connectionId = await this.userService.GetConnectionId(Guid.Parse(this.User.GetId()));
			List<string> ids = new List<string>();
			string isNotCorrect = "";
			ids.Add(connectionId);
			try
			{
				bool isItCalled = false;
				await this.chatHubContext.Clients.GroupExcept(chatId.ToString(), ids).SendAsync("ReceiveMessage", message, chatId);

				string username = this.User.Identity.Name;

				await this.messageService.SaveMessageAsync(message, chatId, username);

				return RedirectToAction("All", "Chat", new { clickedChatId = chatId.ToString() });
			}
			catch (Exception e)
			{
				isNotCorrect = "възникна грешка";
			}
			
			return RedirectToAction("All", "Chat", new { clickedChatId = chatId.ToString()});
		}
	}
}
