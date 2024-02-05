namespace Wealth_Eco_Invest.Services.Messaging.Templates
{
	using System.Text.Encodings.Web;
	using Web.ViewModels.Announce;

	public class AnnounceBuyingTemplate
	{
		public static string Message(string userName, string callbackUrl, AllAnnouncesViewModel announce)
		{
			return @$"
				<div style='text-align: center;'>
					<h2>Thanks for your participation and welcome to the community!</h2>
					<p>Hi {userName},</p>
					<p>
						thanks for your buying! We appreciate that because you help to our community and environment protection!
					</p>
					<div>
						{announce.Title}
					</div>
					<div>
						<img src='{announce.ImageUrl}' style='background-position: center;background-size: cover;width: 22rem;height: 22rem;'/>
					</div>
					<div>
						{announce.Description}
					</div>
			        <div>
			            {announce.Price}
			        </div>
			        <div style='margin-top: 3rem;'>
			           <a style='padding: 16px 20px; background: orange;color: white;border-radius: 3px;text-decoration: none;font-size: 1.2rem;' href='{HtmlEncoder.Default.Encode(callbackUrl)}' target='_blank'>Go back to website</a>
					</div> 
				</div>";
		}
	}
}
