namespace Wealth_Eco_Invest.Web.ViewModels.Announce
{
	using System.ComponentModel.DataAnnotations;
	using Common;
	using Enums;
	using  static Common.GeneralApplicationConstants;
	public class AnnounceQueryViewModel
	{
		public AnnounceQueryViewModel()
		{
			CurrentPage = DefaultPage;
			AnnouncesPerPage = MaxAnnouncesPerPage;
			Categories = new HashSet<string>();
			Announces = new HashSet<AllAnnouncesViewModel>();
		}
		[Display(Name = "")]
		public string? SearchTerm { get; set; }

		public string? Category { get; set; }

		public int AnnouncesPerPage { get; set; }
		public int CurrentPage { get; set; }

		public int TotalAnnounces { get; set; }

		[Display(Name = "Sort Announces By")]
		public AnnounceSorting AnnounceSorting { get; set; }
		public IEnumerable<string> Categories { get; set; }
		public IEnumerable<AllAnnouncesViewModel> Announces { get; set; }

	}
}
