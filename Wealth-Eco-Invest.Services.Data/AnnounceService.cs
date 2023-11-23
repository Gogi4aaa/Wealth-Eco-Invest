namespace Wealth_Eco_Invest.Services.Data
{
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Wealth_Eco_Invest.Data;
    using Wealth_Eco_Invest.Data.Models;
    using Web.ViewModels.Announce;
    using Web.ViewModels.Announce.Enums;
    using static Common.GeneralApplicationConstants;
    public class AnnounceService : IAnnounceService
    {
        private readonly ApplicationDbContext dbContext;

        public AnnounceService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<AllAnnouncesFilteredAndPagedServiceModel> GetAllAnnounces(AnnounceQueryViewModel queryModel)
        {
	        IQueryable<Announce> announcesQuery =
		        this.dbContext
			        .Announces
			        .AsQueryable();

	        if (!string.IsNullOrWhiteSpace(queryModel.Category))
	        {
		        announcesQuery = announcesQuery
					.Where(h => h.Category.Name == queryModel.Category);
	        }

	        if (!string.IsNullOrEmpty(queryModel.SearchTerm) && !string.IsNullOrWhiteSpace(queryModel.SearchTerm))
	        {
		        string wildCard = $"%{queryModel.SearchTerm}%";
		        announcesQuery = announcesQuery.Where(p =>
			        EF.Functions.Like(p.Title, wildCard) || EF.Functions.Like(p.Description, wildCard));
	        }

	        announcesQuery = queryModel.AnnounceSorting switch
	        {
		        AnnounceSorting.Newest => announcesQuery
					.OrderByDescending(h => h.CreatedOn),
		        AnnounceSorting.Oldest => announcesQuery
					.OrderBy(h => h.CreatedOn),
		        AnnounceSorting.PriceAscending => announcesQuery
					.OrderBy(h => h.Price),
		        AnnounceSorting.PriceDescending => announcesQuery
					.OrderByDescending(h => h.Price),
		        _ => announcesQuery
			        .OrderByDescending(h => h.CreatedOn)
	        };

			AllAnnouncesViewModel[] announces = await announcesQuery
				.Where(i => i.IsActive)
		        .Skip((queryModel.CurrentPage - 1) * queryModel.AnnouncesPerPage)
		        .Take(queryModel.AnnouncesPerPage)
		        .Select(a => new AllAnnouncesViewModel()
		        {
			        Id = a.Id,
			        Title = a.Title,
			        Price = a.Price,
			        Description = a.Description,
			        ImageUrl = a.ImageUrl,
		        })
		        .ToArrayAsync();

	        int totalAnnounces = announcesQuery.Count();

	        return new AllAnnouncesFilteredAndPagedServiceModel()
	        {
		        Announces = announces,
		        TotalAnnouncesCount = totalAnnounces
	        };

        }
    }
}
