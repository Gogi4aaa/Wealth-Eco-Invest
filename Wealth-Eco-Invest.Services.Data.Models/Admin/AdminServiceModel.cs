namespace Wealth_Eco_Invest.Services.Data.Models.Admin
{
	using Wealth_Eco_Invest.Web.ViewModels.Admin;

	public class AdminServiceModel : AllUsersViewModel
	{
		public int CurrentPage { get; set; }

		public int TotalUsers { get; set; }

		public int UsersPerPage { get; set; }
	}
}
