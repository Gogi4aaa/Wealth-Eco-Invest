namespace Wealth_Eco_Invest.Services.Data
{
	using Interfaces;
	using Microsoft.EntityFrameworkCore;
	using Wealth_Eco_Invest.Data;
	using Wealth_Eco_Invest.Data.Models;
	using Web.ViewModels.Messages;

	public class MessageService : IMessageService
	{
		private readonly ApplicationDbContext dbContext;
		public MessageService(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<List<MessageViewModel>> GetAllMessagesByChatIdAsync(Guid chatId)
		{
			var all = await this.dbContext
				.Messages
				.Where(x => x.ChatId == chatId) //userFrom == userId || userTo == userId
				.Select(x => new MessageViewModel()
				{
					Message = x.Content,
					TypedOn = x.TypedOn,
					UserFrom = x.Chat.UserFrom,
					UserTo = x.Chat.UserTo,
					ChatId = x.ChatId
				})
				.ToListAsync();

			return all;
		}

		public async Task SaveMessageAsync(string message, Guid chatId, string username)
		{
			var messageToAdd = new Message()
			{
				TypedOn = DateTime.Now,
				Content = message,
				ChatId = chatId,
				OwnerUsername = username
			};

			await this.dbContext.Messages.AddAsync(messageToAdd);
			await this.dbContext.SaveChangesAsync();
		}

		public async Task<string> GetLatestMessage(Guid chatId, Guid userFromId, Guid userToId)
		{
			var message = await this.dbContext.Messages
				.OrderBy(x => x.TypedOn)
				.LastOrDefaultAsync(x =>
					x.ChatId == chatId && x.Chat.UserTo == userToId && x.Chat.UserFrom == userFromId);
			if (message == null)
			{
				return "";
			}
			return message.Content;
		}

		public async Task<string> GetLatestMessageOwner(Guid chatId, Guid userFromId, Guid userToId)
		{
			var message = await this.dbContext.Messages
				.OrderBy(x => x.TypedOn)
				.LastOrDefaultAsync(x =>
					x.ChatId == chatId && x.Chat.UserTo == userToId && x.Chat.UserFrom == userFromId);
			if (message == null)
			{
				return "";
			}
			return message.OwnerUsername;
		}
	}
}
