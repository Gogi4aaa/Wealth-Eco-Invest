namespace Wealth_Eco_Invest.Services.Data
{
	using Interfaces;
	using Microsoft.EntityFrameworkCore;
	using Wealth_Eco_Invest.Data;

	public class UserService : IUserService
	{
		private ApplicationDbContext dbContext;

		public UserService(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		public async Task<string> GetUserNameByIdAsync(Guid userId)
		{
			var user = await this.dbContext.Users
				.FindAsync(userId);

			return user.UserName;
		}

		public async Task<Guid> GetUserIdByUsernameAsync(string username)
		{
			var user = await this.dbContext
				.Users
				.FirstOrDefaultAsync(x => x.UserName == username);
			return user.Id;
		}

		public async Task<bool> IsCurrentUserTyping(Guid chatId, Guid userId)
		{
			var chat = await this.dbContext
				.Chats
				.FindAsync(chatId);

			return chat.UserFrom == userId;
		}

		public async Task<string> GetConnectionId(Guid userId)
		{
			var user = await this.dbContext
				.Users
				.FindAsync(userId);
			return user.ConnectionId;
		}
	}
}
