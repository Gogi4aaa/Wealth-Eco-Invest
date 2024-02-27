namespace Wealth_Eco_Invest.Web.ViewModels.Admin
{
	using Announce;

	public class AllAnnouncesForEachUser
	{
		public AllAnnouncesForEachUser()
		{
			this.Announces = new HashSet<AllAnnouncesForAdminViewModel>();
		}

		public ICollection<AllAnnouncesForAdminViewModel> Announces { get; set; }
	}
}
