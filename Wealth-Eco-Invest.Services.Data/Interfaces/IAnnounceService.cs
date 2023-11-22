namespace Wealth_Eco_Invest.Services.Data.Interfaces
{
    using Web.ViewModels.Announce;

    public interface IAnnounceService
    {
        Task<IEnumerable<AllAnnouncesViewModel>> AllAnnounces();

    }
}
