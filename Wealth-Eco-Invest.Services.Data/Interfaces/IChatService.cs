using Wealth_Eco_Invest.Web.ViewModels.Messages;

namespace Wealth_Eco_Invest.Services.Data.Interfaces
{
	public interface IChatService
	{
		Task<List<AllChatsViewModel>> GetAllChatsByUserId(Guid userId);

		Task AddChatAsync(Guid userFrom, Guid userTo, Guid announceId);

		Task<ChatViewModel> GetChatByChatIdAsync(Guid chatId);

		Task<bool> IsChatAlreadyExist(Guid currentUserId, Guid ownerId);
	}
}
