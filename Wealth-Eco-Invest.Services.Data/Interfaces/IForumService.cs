namespace Wealth_Eco_Invest.Services.Data.Interfaces
{
	using Web.ViewModels.Messages;

	public interface IForumService
	{
		Task<List<AllChatsViewModel>> GetAllForumsByUserIdAsync(Guid userId);

	}
}
