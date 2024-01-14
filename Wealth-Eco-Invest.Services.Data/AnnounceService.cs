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

        public async Task<AllAnnouncesFilteredAndPagedServiceModel> GetAllAnnouncesAsync(AnnounceQueryViewModel queryModel)
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
					StartDate = a.StartDate,
			        Description = a.Description,
			        ImageUrl = a.ImageUrl,
		        })
		        .ToArrayAsync();

	        int totalAnnounces = announcesQuery.Count(x=> x.IsActive);

	        return new AllAnnouncesFilteredAndPagedServiceModel()
	        {
		        Announces = announces,
		        TotalAnnouncesCount = totalAnnounces
	        };

        }

        public async Task AddAnnounceAsync(AnnounceFormModel model)
        {
	        Announce announce = new Announce()
	        {
		        Title = model.Title,
				Price = model.Price,
				Description = model.Description,
				StartDate = model.StartDate,
				ImageUrl = model.ImageUrl,
				IsActive = true,
				CategoryId = model.CategoryId,
				CreatedOn = DateTime.UtcNow,
				UserId = model.UserId,
	        };

	        await this.dbContext.AddAsync(announce);
	        await this.dbContext.SaveChangesAsync();
        }

        public async Task<AnnounceDetailsViewModel> GetAnnounceForDetailsByIdAsync(Guid announceId)
        {
	        return await this.dbContext
		        .Announces
		        .Where(x => x.Id == announceId && x.IsActive)
		        .Select(model => new AnnounceDetailsViewModel()
		        {
					Id = model.Id,
			        Title = model.Title,
			        Price = model.Price,
			        Description = model.Description,
			        ImageUrl = model.ImageUrl,
			        Owner = model.User.UserName,
					StartDate = model.StartDate,
		        })
		        .FirstAsync();
        }

        public async Task DeleteAnnounceByIdAsync(Guid announceId)
        {
	        Announce itemToDelete = await this.dbContext
		        .Announces
		        .Where(x => x.IsActive)
		        .FirstAsync(x=> x.Id == announceId);

	        itemToDelete.IsActive = false;

	        await this.dbContext.SaveChangesAsync();
        }

        public async Task EditAnnounceByIdAndFormModelAsync(Guid announceId, AnnounceFormModel model)
        {
	        Announce announce = await this.dbContext
		        .Announces
		        .Where(a => a.IsActive)
		        .FirstAsync(x => x.Id == announceId);

			announce.Title = model.Title;
			announce.StartDate = model.StartDate;
			announce.Description = model.Description;
			announce.ImageUrl = model.ImageUrl;
			announce.Price = model.Price;
			announce.CategoryId = model.CategoryId;

			await this.dbContext.SaveChangesAsync();
        }

        public async Task<AnnounceFormModel> GetAnnounceForEditAsync(Guid announceId)
        {
	        Announce announce = await this.dbContext
		        .Announces
		        .Where(x => x.IsActive)
		        .FirstAsync(x => x.Id == announceId);

	        return new AnnounceFormModel()
	        {
		        Title = announce.Title,
				Description = announce.Description,
				ImageUrl = announce.ImageUrl,
				Price = announce.Price,
				CategoryId = announce.CategoryId,
				StartDate = announce.StartDate
	        };
        }

        public async Task<ApplicationUser> GetUserByAnnounceId(Guid announceId)
        {
	        Announce announce = await this.dbContext
		        .Announces
		        .Where(x=> x.IsActive)
		        .FirstAsync(x => x.Id == announceId);

	        var user = await this.dbContext.Users.FindAsync(announce!.UserId);

	        return user!;
        }

        public async Task<IEnumerable<AllAnnouncesViewModel>> GetAllByUserIdAsync(Guid userId)
        {
            var allAnnounces = await this.dbContext
                .Announces
                .Where(x => x.IsActive &&
                            x.UserId == userId)
                .Select(x => new AllAnnouncesViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
					StartDate = x.StartDate,
                    ImageUrl = x.ImageUrl,
                    Price = x.Price,
                })
                .ToArrayAsync();

            return allAnnounces;
        }

        
    }
}
