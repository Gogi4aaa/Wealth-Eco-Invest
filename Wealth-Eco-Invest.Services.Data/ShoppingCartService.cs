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
                    CreatedOn = x.Announce.CreatedOn,
                    Count = x.Quantity,
					StartDate = x.Announce.StartDate,
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

				var all = await this.GetAllAnnouncesForUser(userId);
				foreach (var currAnnounce in all.Announces)
				{
		            if (currAnnounce.Id == announceId)
		            {
			            cart.Quantity = currAnnounce.Count + 1;
			            await this.UpdateAnnounceToUser(announceId, userId, cart.Quantity);
			            return;
		            }
				}
	            this.dbContext.Carts.Add(cart);
	            await this.dbContext.SaveChangesAsync();
	            
	    }

        public async Task<AllAnnouncesViewModel> GetAnnounceByAnnounceId(Guid announceId, Guid userId)
        {
	        var announce = await this.dbContext
		        .Carts
		        .Where(x => x.AnnounceId == announceId && x.BuyerId == userId)
		        .Select(announce => new AllAnnouncesViewModel()
		        {
			        Id = announce.AnnounceId,
			        Title = announce.Announce.Title,
			        Description = announce.Announce.Description,
			        ImageUrl = announce.Announce.ImageUrl,
			        Price = announce.Announce.Price,
			        CreatedOn = announce.Announce.CreatedOn,
			        Count = announce.Quantity,
					StartDate = announce.Announce.StartDate,
		        })
		        .ToArrayAsync();

	        return announce.First();
        }
		public async Task UpdateAnnounceToUser(Guid announceId, Guid userId, int announceCount)
        {
	        var cart = await this.dbContext
				.Carts
				.FirstAsync(x => x.AnnounceId == announceId && x.BuyerId == userId);
			cart.Quantity = announceCount;

			await this.dbContext.SaveChangesAsync();
		}

        public async Task DeleteAnnounceToUser(Guid announceId, Guid userId)
        {
	        var cartItem = await this.dbContext
		        .Carts
		        .FirstAsync(x => x.AnnounceId == announceId && x.BuyerId == userId);

	        this.dbContext.Remove(cartItem);
	        await this.dbContext.SaveChangesAsync();
        }
    }
}
