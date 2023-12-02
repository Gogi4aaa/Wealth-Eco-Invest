namespace Wealth_Eco_Invest.Services.Messaging
{
	public interface IEmailSender
	{
		Task SendEmailAsync(string email, string subject, string message);
	}
}
