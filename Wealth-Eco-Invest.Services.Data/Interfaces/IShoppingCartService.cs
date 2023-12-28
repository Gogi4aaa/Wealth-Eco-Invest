namespace Wealth_Eco_Invest.Services.Data.Interfaces
{
    using Wealth_Eco_Invest.Data.Models;
    using Web.ViewModels.ShoppingCart;

    public interface IShoppingCartService
    {
        Task<ShoppingCartViewModel> GetAllAnnouncesForUser(Guid id);

        Task AddAnnounceToUser(Guid announceId, Guid userId);

        Task UpdateAnnounceToUser(Guid announceId, Guid userId, int announceCount);

        Task DeleteAnnounceToUser(Guid announceId, Guid userId);
    }
}
