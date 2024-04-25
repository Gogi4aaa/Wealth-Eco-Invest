namespace Wealth_Eco_Invest.Services.Messaging
{
	using System.Net;
	using System.Net.Mail;
	using Mailjet.Client;
	using Mailjet.Client.Resources;
	using MimeKit;
	using MimeKit.Text;
	using Newtonsoft.Json.Linq;
	using static Common.GeneralApplicationConstants;

	public class EmailSender : IEmailSender
	{
		public async Task SendEmailAsync(string email, string subject, string message)
		{
			MailjetClient client = new MailjetClient("da10bcef6850eadf29c2f3b107ba5641", "00a9193a17e4554a5095df26f4e47832");
			MailjetRequest request = new MailjetRequest()
				{
					Resource = Send.Resource,
				}
				.Property(Send.FromEmail, EmailFrom)
				.Property(Send.FromName, "Mailjet Pilot")
				.Property(Send.Subject, subject)
				.Property(Send.TextPart, message)
				.Property(Send.HtmlPart, "<h3>Dear passenger, welcome to <a href=\"https://www.mailjet.com/\">Mailjet</a>!<br />May the delivery force be with you!")
				.Property(Send.To, $"Name {email}, Name2 {email}")
				.Property(Send.Cc, $"Name3 {email}>");
			MailjetResponse response = await client.PostAsync(request);
			;
			//using var client = new SmtpClient("smtp.gmail.com")
			//{
			//	Port = 587,
			//	EnableSsl = true,
			//	Credentials = new NetworkCredential(SmtpMail,SmtpPassword),
			//};
			//await client.SendMailAsync(
			//	new MailMessage(from: EmailFrom,
			//					to: email,
			//					subject,
			//					message));
		}
	}
}
