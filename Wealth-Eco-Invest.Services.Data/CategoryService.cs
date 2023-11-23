namespace Wealth_Eco_Invest.Services.Data
{
	using Interfaces;
	using Microsoft.EntityFrameworkCore;
	using Wealth_Eco_Invest.Data;
	using Wealth_Eco_Invest.Data.Models;

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
	}
}
