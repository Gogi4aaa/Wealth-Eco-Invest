namespace Wealth_Eco_Invest.Services.Data.Interfaces
{
	using Web.ViewModels.Messages;

	public interface IMessageService
	{
		Task<List<MessageViewModel>> GetAllMessagesByChatIdAsync(Guid chatId);

		Task SaveMessageAsync(string message, Guid chatId);

	}
}
