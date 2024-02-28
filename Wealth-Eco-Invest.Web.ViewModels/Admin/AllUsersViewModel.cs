namespace Wealth_Eco_Invest.Web.ViewModels.Admin
{
	public class AllUsersViewModel
	{
		public AllUsersViewModel()
		{
			this.Users = new HashSet<AllAnnouncesForAdminViewModel>();
		}

		public ICollection<AllAnnouncesForAdminViewModel> Users { get; set; }
	}
}
