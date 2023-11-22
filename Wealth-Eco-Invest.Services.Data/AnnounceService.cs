namespace Wealth_Eco_Invest.Services.Data
{
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Wealth_Eco_Invest.Data;
    using Web.ViewModels.Announce;

    public class AnnounceService : IAnnounceService
    {
        private readonly ApplicationDbContext dbContext;

        public AnnounceService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<AllAnnouncesViewModel>> AllAnnounces()
        {
            AllAnnouncesViewModel[] elements = await this.dbContext
                .Announces
                .Where(i => i.IsActive)
                .Select(a => new AllAnnouncesViewModel()
                {
                    Id = a.Id,
                    Title = a.Title,
                    Price = a.Price,
                    Description = a.Description,
                    ImageUrl = a.ImageUrl,
                    IsRented = true,
                })
                .ToArrayAsync();

            return elements;

        }
    }
}
