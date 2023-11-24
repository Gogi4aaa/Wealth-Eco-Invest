namespace Wealth_Eco_Invest.Services.Data.Interfaces
{
	using Models;
	using Wealth_Eco_Invest.Web.ViewModels.Announce;

	public interface IAnnounceService
    {
        Task<AllAnnouncesFilteredAndPagedServiceModel> GetAllAnnouncesAsync(AnnounceQueryViewModel queryModel);

        Task AddAnnounceAsync(AnnounceFormModel model);

        Task<AnnounceDetailsViewModel> GetAnnounceForDetailsByIdAsync(Guid id);

        Task DeleteAnnounceByIdAsync(Guid id);
    }
}
