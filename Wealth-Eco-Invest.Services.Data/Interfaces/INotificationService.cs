using Wealth_Eco_Invest.Web.ViewModels.Announce;

namespace Wealth_Eco_Invest.Services.Data.Interfaces
{
	public interface INotificationService
	{
		Task SendEmailNotification(AllAnnouncesViewModel announce, Guid userId);
	}
}
