namespace Wealth_Eco_Invest.Web.ViewModels.Messages
{
	public class ChatViewModel
	{
		public ChatViewModel()
		{
			this.Messages = new HashSet<MessageViewModel>();
		}

		public string MessageInput { get; set; }
		public Guid Id { get; set; }
		public Guid UserTo { get; set; }

		public Guid UserFrom { get; set; }

		public ICollection<MessageViewModel> Messages { get; set; }
	}
}
