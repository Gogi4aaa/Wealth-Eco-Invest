namespace Wealth_Eco_Invest.Services.Models.Announce
{
	using Web.ViewModels.Announce;

	public class AllAnnouncesFilteredAndPagedServiceModel
	{
		public AllAnnouncesFilteredAndPagedServiceModel()
		{
			Announces = new HashSet<AllAnnouncesViewModel>();
		}
		public int TotalAnnouncesCount { get; set; }
		public IEnumerable<AllAnnouncesViewModel> Announces { get; set; }
	}
}
