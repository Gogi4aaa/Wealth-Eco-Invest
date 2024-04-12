namespace Wealth_Eco_Invest.Services.Data
{
	using Interfaces;
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
	}
}
