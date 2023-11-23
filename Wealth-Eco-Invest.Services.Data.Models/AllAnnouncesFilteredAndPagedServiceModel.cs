namespace Wealth_Eco_Invest.Services.Data.Models
{
	using Web.ViewModels.Announce;

	public class AllAnnouncesFilteredAndPagedServiceModel
	{
		public AllAnnouncesFilteredAndPagedServiceModel()
		{
			this.Announces = new HashSet<AllAnnouncesViewModel>();
		}
		public int TotalAnnouncesCount { get; set; }
		public IEnumerable<AllAnnouncesViewModel> Announces { get; set; }
	}
}
