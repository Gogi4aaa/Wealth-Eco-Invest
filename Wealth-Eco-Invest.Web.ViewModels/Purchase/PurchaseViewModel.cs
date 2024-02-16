namespace Wealth_Eco_Invest.Web.ViewModels.Purchase
{
	using Announce;

	public class PurchaseViewModel
	{
		public IEnumerable<AllAnnouncesViewModel> Announces { get; set; } = null!;
	}
}
