namespace Wealth_Eco_Invest.Services.Data
{
	using Interfaces;
	using Web.ViewModels.Messages;

	public class MessageService : IMessageService
	{
		public Task<List<AllMessagesViewModel>> GetAllMessagesByUserAsync(Guid userId)
		{
			throw new NotImplementedException();
		}
	}
}
