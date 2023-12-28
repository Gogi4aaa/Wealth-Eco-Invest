namespace Wealth_Eco_Invest.Web.ViewModels.ShoppingCart
{
	using Announce;
	using Data.Models;
    public class ShoppingCartViewModel
    {
        public IEnumerable<AllAnnouncesViewModel> Announces { get; set; } = null!;
    }
}
