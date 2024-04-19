namespace Wealth_Eco_Invest.Web.ViewModels.Messages
{
	public class MessageViewModel
	{
		public string Name { get; set; }

		public string Message { get; set; }

		public DateTime TypedOn { get; set; }

		public Guid UserTo { get; set; }

		public Guid ChatUserFrom { get; set; }

		public Guid ChatUserTo { get; set; }

		public Guid UserFrom { get; set; }
		public string Owner { get; set; }	
		public Guid ChatId { get; set; }
	}
}
