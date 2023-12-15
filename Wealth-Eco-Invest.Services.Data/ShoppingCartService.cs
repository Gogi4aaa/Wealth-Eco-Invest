namespace Wealth_Eco_Invest.Services.Data
{
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Wealth_Eco_Invest.Data;
    using Wealth_Eco_Invest.Data.Models;
    using Web.ViewModels.Announce;
    using Web.ViewModels.ShoppingCart;

    public class ShoppingCartService : IShoppingCartService
    {
        private readonly ApplicationDbContext dbContext;

        public ShoppingCartService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<ShoppingCartViewModel> GetAllAnnouncesForUser(Guid id)
        {
	        var all = await this.dbContext
		        .Carts
		        .Where(x => x.BuyerId == id)
		        .Select(x=> new AllAnnouncesViewModel()
		        {
                    Id = x.AnnounceId,
                    Price = x.Announce.Price,
                    Description = x.Announce.Description,
                    ImageUrl = x.Announce.ImageUrl,
                    Title = x.Announce.Title,
		        })
		        .ToArrayAsync();

	        return new ShoppingCartViewModel()
	        {
		        Announces = all
	        };
        }

        public async Task AddAnnounceToUser(Guid announceId, Guid userId)
        {
            Announce? announce = await this.dbContext.Announces.FirstAsync(a => a.Id == announceId);

            var user = await this.dbContext.Users.FindAsync(userId!);
            var cart = new Cart()
            {
	            AnnounceId = announce.Id,
	            BuyerId = user.Id,
	            Quantity = 1,//TODO must be edited
            };
            this.dbContext.Carts.Add(cart);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
