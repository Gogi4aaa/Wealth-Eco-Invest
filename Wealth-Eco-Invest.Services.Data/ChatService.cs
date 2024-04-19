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
					ChatId = x.Id,
					AnnounceName = x.Announce.Title,

				})
				.OrderByDescending(x => x.StartedOn)
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

		public async Task<bool> IsChatAlreadyExist(Guid currentUserId, Guid ownerId)
		{
			if (currentUserId == ownerId)
			{
				return true;
			}
			return await this.dbContext
				.Chats
				.AnyAsync(x => (x.UserFrom == currentUserId && x.UserTo == ownerId) || (x.UserFrom == ownerId && x.UserTo == currentUserId));
		}

		public async Task<Guid> GetLatestChatIdAsync(Guid userId)
		{
			var chat = await this.dbContext
				.Chats
				.Where(x => x.UserFrom == userId || x.UserTo == userId)
				.OrderBy(x => x.StartedOn)
				.LastOrDefaultAsync();

			return chat.Id;
		}
	}
}