namespace Wealth_Eco_Invest.Services.Data.Interfaces
{
	using Wealth_Eco_Invest.Data.Models;

	public interface ICategoryService
	{
		Task<IEnumerable<string>> AllCategoriesNamesAsync();
	}
}
