namespace Wealth_Eco_Invest.Services.Messaging.Templates
{
	using System.Text.Encodings.Web;
	using Wealth_Eco_Invest.Web.ViewModels.Announce;


	public class AnnounceStartingTimeInfoTemplate
	{
		public static string Message(string userName, AllAnnouncesViewModel announce)
		{
			return @$"
				<div style='text-align: center;'>
					<h2>Thanks for your participation and welcome to the community!</h2>
					<p>Hi {userName},</p>
					<p>
						we remind you that you have only 24 hours before event starting!
						Check the calendar for more information! 
					</p>
					<div>
						Event starts at: {announce.StartDate}
					</div>
				</div>";
		}
	}
}
