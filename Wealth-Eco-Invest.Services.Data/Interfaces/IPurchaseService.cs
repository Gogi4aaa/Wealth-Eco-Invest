namespace Wealth_Eco_Invest.Services.Data.Interfaces
{
	using Wealth_Eco_Invest.Data.Models;
	using Wealth_Eco_Invest.Web.ViewModels.Purchase;

	public interface IPurchaseService
	{
		Task PurchaseAnnounceAsync(Guid announceId, Guid userId);

		Task<PurchaseViewModel> GetAllPurchasedAnnouncesByUserIdAsync(Guid id);

		Task<bool> CheckIsThisAnnounceIsAlreadyBoughtByCurrentUser(Guid id, Guid userId);
	}
}
