namespace Wealth_Eco_Invest.Services.Data.Interfaces
{
	using Web.ViewModels.Messages;

	public interface IMessageService
	{
		Task<List<MessageViewModel>> GetAllMessagesByChatIdAsync(Guid chatId);

		Task SaveMessageAsync(string message, Guid chatId, string username);

		Task<string> GetLatestMessage(Guid chatId, Guid userFromId, Guid? userToId);

		Task<string> GetLatestMessageOwner(Guid chatId, Guid userFromId, Guid? userToId);

	}
}
