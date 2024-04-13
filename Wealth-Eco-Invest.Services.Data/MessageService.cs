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
				})
				.ToListAsync();

			return all;
		}

		public async Task SaveMessageAsync(string message, Guid chatId)
		{
			var messageToAdd = new Message()
			{
				TypedOn = DateTime.Now,
				Content = message,
				ChatId = chatId,
			};

			await this.dbContext.Messages.AddAsync(messageToAdd);
			await this.dbContext.SaveChangesAsync();
		}
	}
}
