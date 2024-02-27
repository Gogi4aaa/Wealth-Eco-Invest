namespace Wealth_Eco_Invest.Services.Data
{
	using Interfaces;
	using Microsoft.EntityFrameworkCore;
	using Wealth_Eco_Invest.Data;
	using Wealth_Eco_Invest.Data.Models;
	using Web.ViewModels.Category;

	public class CategoryService : ICategoryService
	{
		private readonly ApplicationDbContext dbContext;

		public CategoryService(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		public async Task<IEnumerable<string>> AllCategoriesNamesAsync()
		{
			string[] allCategories = await this.dbContext
				.Categories
				.Select(x=> x.Name)
				.ToArrayAsync();

			return allCategories;
		}

		public async Task<IEnumerable<AnnounceSelectCategoryFormModel>> AllCategoriesAsync()
		{
			IEnumerable<AnnounceSelectCategoryFormModel> allCategories = await dbContext
				.Categories
				.AsNoTracking()
				.Select(c => new AnnounceSelectCategoryFormModel()
				{
					Id = c.Id,
					Name = c.Name,
				})
				.ToArrayAsync();

			return allCategories;
		}

		public async Task<string> GetCategoryNameByCategoryIdAsync(int id)
		{
			Category? category = await this.dbContext
				.Categories
				.FirstOrDefaultAsync(x => x.Id == id);

			return category.Name;
		}
	}
}
