﻿namespace Wealth_Eco_Invest.Services.Data.Interfaces
{
    using Models;
    using Wealth_Eco_Invest.Data.Models;
    using Wealth_Eco_Invest.Services.Data.Models.Announces;
    using Wealth_Eco_Invest.Web.ViewModels.Announce;

    public interface IAnnounceService
    {
        Task<AllAnnouncesFilteredAndPagedServiceModel> GetAllAnnouncesAsync(AnnounceQueryViewModel queryModel);

        Task AddAnnounceAsync(AnnounceFormModel model);

        Task<AnnounceDetailsViewModel> GetAnnounceForDetailsByIdAsync(Guid announceId);

        Task DeleteAnnounceByIdAsync(Guid announceId);

        Task EditAnnounceByIdAndFormModelAsync(Guid announceId, AnnounceFormModel model);

        Task<AnnounceFormModel> GetAnnounceForEditAsync(Guid announceId);

        Task<ApplicationUser> GetUserByAnnounceId(Guid announceId);

        Task<IEnumerable<AllAnnouncesViewModel>> GetAllByUserIdAsync(Guid userId);

    }
}
