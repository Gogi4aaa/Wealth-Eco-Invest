namespace Wealth_Eco_Invest.Services.Data
{
	using Interfaces;
	using Microsoft.EntityFrameworkCore;
	using Wealth_Eco_Invest.Data;
	using Wealth_Eco_Invest.Data.Models;
	using Wealth_Eco_Invest.Web.ViewModels.Messages;

	public class ChatService : IChatService
	{
		private readonly ApplicationDbContext dbContext;

		public ChatService(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		public async Task<List<AllChatsViewModel>> GetAllChatsByUserId(Guid userId)
		{
			var all = await this.dbContext
				.Chats
				.Where(x => x.UserFrom == userId || x.UserTo == userId)
				.Select(x => new AllChatsViewModel()
				{
					UserTo = x.UserTo,
					UserFrom = x.UserFrom,
					StartedOn = x.StartedOn,
					ChatId = x.Id
				})
				.ToListAsync();

			return all;
		}

		public async Task AddChatAsync(Guid userFrom, Guid userTo, Guid announceId)
		{
			var chat = new Chat()
			{
				UserTo = userTo,
				UserFrom = userFrom,
				StartedOn = DateTime.Now,
				AnnounceId = announceId,
			};

			await this.dbContext.Chats.AddAsync(chat);
			await this.dbContext.SaveChangesAsync();
		}

		public async Task<ChatViewModel> GetChatByChatIdAsync(Guid chatId)
		{
			var chat = await this.dbContext
				.Chats
				.FindAsync(chatId);
			return new ChatViewModel()
			{
				UserTo = chat.UserTo,
				UserFrom = chat.UserFrom,
				Id = chat.Id,
			};
		}
	}
}
