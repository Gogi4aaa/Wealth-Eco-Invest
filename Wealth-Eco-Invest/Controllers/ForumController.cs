using Microsoft.AspNetCore.Mvc;

namespace Wealth_Eco_Invest.Controllers
{
	using Services.Data.Interfaces;
	using Wealth_Eco_Invest.Web.ViewModels.Messages;
	using Web.Infrastructure.Extensions;

	public class ForumController : Controller
	{
		private readonly IForumService forumService;
		private readonly IMessageService messageService;
		private readonly IUserService userService;
		private readonly IAnnounceService announceService;
		public ForumController(IForumService forumService, IMessageService messageService, IUserService userService, IAnnounceService announceService)
		{
			this.forumService = forumService;
			this.messageService = messageService;
			this.userService = userService;
			this.announceService = announceService;
		}
		public async Task<IActionResult> All(string clickedChatId = "")
		{
			var allChats = await this.forumService.GetAllForumsByUserIdAsync(Guid.Parse(this.User.GetId()));
			//copy and paste code for latest message and others from /chat/all from controller
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
					chat.Message = "Все още няма съобщения в този форум!";
				}
			}

			Guid? chatId = new Guid();
			if (clickedChatId == "")
			{
				if (allChats.Count == 0)
				{
					chatId = null;
				}
				else if (allChats.Count == 1)
				{
					chatId = allChats[0].ChatId;
				}
				else
				{
					var latest = allChats.OrderByDescending(x => x.StartedOn)
						.LastOrDefault();
					chatId = latest.ChatId;
				}
				
			}
			else
			{
				chatId = Guid.Parse(clickedChatId);
			}

			var allMessages = new List<MessageViewModel>();
			if (allChats.Count > 0)
			{
				allMessages = await this.messageService.GetAllMessagesByChatIdAsync(chatId);
				foreach (var messageViewModel in allMessages)
				{
					messageViewModel.UserFrom = await this.userService.GetUserIdByUsernameAsync(messageViewModel.Owner);
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
	}
}