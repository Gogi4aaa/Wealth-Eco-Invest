namespace Wealth_Eco_Invest.Services.Data.Interfaces
{
	public interface IUserService
	{
		Task<string> GetUserNameByIdAsync(Guid userId);

		Task<Guid> GetUserIdByUsernameAsync(string username);

		Task<bool> IsCurrentUserTyping(Guid chatId, Guid userId);

		Task<string> GetConnectionId(Guid userId);
	}
}
