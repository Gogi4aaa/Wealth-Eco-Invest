using Wealth_Eco_Invest.Data.Models;
using Wealth_Eco_Invest.Web.ViewModels.Admin;

namespace Wealth_Eco_Invest.Services.Data.Interfaces
{
	public interface IAdminService
	{
		Task<ApplicationUser> IsUserAdmin(Guid userId);

		Task<AllAnnouncesForEachUser> GetAllAnnouncesForEachUser();

		Task<IEnumerable<AllAnnouncesForAdminViewModel>> GetAllAnnouncesByUserIdAsync(Guid userId);
	}
}
