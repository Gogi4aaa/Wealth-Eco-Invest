namespace Wealth_Eco_Invest.Services.Data.Interfaces
{
	using Wealth_Eco_Invest.Data.Models;
	using Wealth_Eco_Invest.Web.ViewModels.Category;

	public interface ICategoryService
	{
		Task<IEnumerable<string>> AllCategoriesNamesAsync();
		Task<IEnumerable<AnnounceSelectCategoryFormModel>> AllCategoriesAsync();
		Task<string> GetCategoryNameByCategoryIdAsync(int id);
	}
}
