namespace Wealth_Eco_Invest.Services.Data.Interfaces
{
	using Web.ViewModels.Messages;

	public interface IMessageService
	{
		Task<List<AllMessagesViewModel>> GetAllMessagesByUserAsync(Guid userId);
	}
}
