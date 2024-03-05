using Wealth_Eco_Invest.Data.Models;
using Wealth_Eco_Invest.Web.ViewModels.Admin;

namespace Wealth_Eco_Invest.Services.Data.Interfaces
{
	public interface IAdminService
	{
		Task<bool> IsUserAdmin(Guid userId);
		Task<ApplicationUser> GetUser(Guid userId);

		Task<AllUsersViewModel> GetAllUsers();

		Task<IEnumerable<AllAnnouncesForAdminViewModel>> GetAllAnnouncesByUserIdAsync(Guid userId);
	}
}
