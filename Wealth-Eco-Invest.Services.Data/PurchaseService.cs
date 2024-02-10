namespace Wealth_Eco_Invest.Services.Data
{
	using Interfaces;
	using Microsoft.EntityFrameworkCore;
	using Wealth_Eco_Invest.Data;
	using Wealth_Eco_Invest.Data.Models;
	using Wealth_Eco_Invest.Web.ViewModels.ShoppingCart;
	using Web.ViewModels.Announce;
	using Web.ViewModels.Purchase;

	public class PurchaseService : IPurchaseService
	{
		private readonly ApplicationDbContext dbContext;
		public PurchaseService(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		public async Task PurchaseAnnounceAsync(Guid announceId, Guid userId)
		{
			var purchase = new Purchase()
			{
				AnnounceId = announceId,
				BuyerId = userId,
			};

			await this.dbContext.Purchases.AddAsync(purchase);
			await this.dbContext.SaveChangesAsync();
		}

		public async Task<PurchaseViewModel> GetAllPurchasedAnnouncesByUserIdAsync(Guid id)
		{
			var purchases = await this.dbContext
				.Purchases
				.Where(x => x.BuyerId == id)
				.Select(x => new AllAnnouncesViewModel()
				{
					Id = x.Id,
					Price = x.Announce.Price,
					Description = x.Announce.Description,
					ImageUrl = x.Announce.ImageUrl,
					Title = x.Announce.Title,
					CreatedOn = x.Announce.CreatedOn,
					Count = 1,
					StartDate = x.Announce.StartDate,
				})
				.ToArrayAsync();

			return new PurchaseViewModel()
			{
				Announces = purchases
			};
		}
	}
}
