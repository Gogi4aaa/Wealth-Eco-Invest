namespace Wealth_Eco_Invest.Services.Data.Interfaces
{
	public interface IUserService
	{
		Task<string> GetUserNameByIdAsync(Guid userId);
	}
}
