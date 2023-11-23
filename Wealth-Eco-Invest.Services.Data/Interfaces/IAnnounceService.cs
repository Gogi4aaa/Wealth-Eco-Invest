namespace Wealth_Eco_Invest.Services.Data.Interfaces
{
	using Models;
	using Wealth_Eco_Invest.Web.ViewModels.Announce;

	public interface IAnnounceService
    {
        Task<AllAnnouncesFilteredAndPagedServiceModel> GetAllAnnounces(AnnounceQueryViewModel queryModel);

    }
}
