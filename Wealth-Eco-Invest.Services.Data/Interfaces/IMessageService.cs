namespace Wealth_Eco_Invest.Services.Data.Interfaces
{
	using Web.ViewModels.Messages;

	public interface IMessageService
	{
		Task<List<AllChatsViewModel>> GetAllChatsByUserId(Guid userId);
		Task<List<MessageViewModel>> GetAllMessagesByChatIdAsync(Guid chatId);

	}
}
