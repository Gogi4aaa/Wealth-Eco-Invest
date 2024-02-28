namespace Wealth_Eco_Invest.Services.Data.Interfaces
{
	using Web.ViewModels.Category;

	public interface ICategoryService
	{
		Task<IEnumerable<string>> AllCategoriesNamesAsync();
		Task<IEnumerable<AnnounceSelectCategoryFormModel>> AllCategoriesAsync();
	}
}
