namespace Wealth_Eco_Invest.Web.ViewModels.Messages
{
	public class AllChatsAndMessagesViewModel
	{
		public Guid ChatId { get; set; }
		public List<AllChatsViewModel> Chats { get; set; }

		public List<MessageViewModel> Messages { get; set; }
	}
}
