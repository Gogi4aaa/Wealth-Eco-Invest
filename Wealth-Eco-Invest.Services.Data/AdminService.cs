namespace Wealth_Eco_Invest.Services.Data
{
	using Wealth_Eco_Invest.Data.Models;
	using Interfaces;
	using Wealth_Eco_Invest.Data;
	using Microsoft.EntityFrameworkCore;
	using Web.ViewModels.Admin;
	using Web.ViewModels.Announce;

	public class AdminService : IAdminService
	{
		private readonly ApplicationDbContext dbContext;
		public AdminService(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		public async Task<ApplicationUser> IsUserAdmin(Guid userId)
		{
			ApplicationUser user = await dbContext.Users.FirstAsync(x => x.Id == userId);
			return user;
		}

		public async Task<AllAnnouncesForEachUser> GetAllAnnouncesForEachUser()
		{

			AllAnnouncesForAdminViewModel[] allAnnounces = await this.dbContext
				.Users
				.Select(x => new AllAnnouncesForAdminViewModel()
				{
					UserId = x.Id,
					Username = x.UserName,
					Email = x.Email,
				})
				.ToArrayAsync();
			return new AllAnnouncesForEachUser()
			{
				Announces = allAnnounces
			};
		}

		public async Task<IEnumerable<AllAnnouncesForAdminViewModel>> GetAllAnnouncesByUserIdAsync(Guid userId)
		{
			var allAnnounces = await this.dbContext
				.Announces
				.Where(x => x.UserId == userId)
				.Select(x => new AllAnnouncesForAdminViewModel()
				{
					Id = x.Id,
					Title = x.Title,
					Description = x.Description,
					StartDate = x.StartDate,
					ImageUrl = x.ImageUrl,
					Price = x.Price,
					IsActive = x.IsActive,
					Category = x.Category.Name,
					CreatedOn = x.CreatedOn
				})
				.ToArrayAsync();

			return allAnnounces;
		}
	}
}
