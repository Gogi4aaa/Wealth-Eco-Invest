namespace Wealth_Eco_Invest.Web.ViewModels.Messages
{
	public class AllMessagesViewModel
	{
		public string Name { get; set; }

		public string Message { get; set; }

		public DateTime TypedOn { get; set; }

		public Guid UserTo { get; set; }

		public Guid UserFrom { get; set; }
	}
}
