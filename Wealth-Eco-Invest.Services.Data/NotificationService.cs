namespace Wealth_Eco_Invest.Services.Data
{
	using Hangfire;
	using Microsoft.EntityFrameworkCore;
	using Wealth_Eco_Invest.Data;
	using Messaging;
	using Messaging.Templates;
	using Web.ViewModels.Announce;
	using Interfaces;

	public class NotificationService : INotificationService
	{
		private readonly IEmailSender emailSender;
		private readonly ApplicationDbContext dbContext;

		public NotificationService(IEmailSender emailSender, ApplicationDbContext dbContext)
		{
			this.emailSender = emailSender;
			this.dbContext = dbContext;
		}

		public async Task SendEmailNotification(AllAnnouncesViewModel announce, Guid userId)
		{
			var user = await this.dbContext
				.Users
				.FirstOrDefaultAsync(x => x.Id == userId);
			DateTime targetTime = announce.StartDate;
			var notificationTime = targetTime.AddHours(-24);
			BackgroundJob.Schedule(
				() => this.emailSender.SendEmailAsync(user!.Email, "Announce Notification",
					AnnounceStartingTimeInfoTemplate.Message(user.UserName, announce)), notificationTime);
		}
	}
}