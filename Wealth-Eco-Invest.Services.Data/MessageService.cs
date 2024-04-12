namespace Wealth_Eco_Invest.Services.Data
{
	using Interfaces;
	using Microsoft.EntityFrameworkCore;
	using Wealth_Eco_Invest.Data;
	using Web.ViewModels.Messages;

	public class MessageService : IMessageService
	{
		private readonly ApplicationDbContext dbContext;
		public MessageService(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public Task<List<AllMessagesViewModel>> GetAllMessagesByUserAsync(Guid userId)
		{
			var all = this.dbContext
				.Chats
				.Where(x => x.UserFrom == userId) //userFrom == userId || userTo == userId
				.Select(x => new AllMessagesViewModel()
				{
					Message = x.Message,
					TypedOn = x.TypedOn,
					UserFrom = x.UserFrom,
					UserTo = x.UserTo,
				})
				.ToListAsync();

			return all;
		}
	}
}
