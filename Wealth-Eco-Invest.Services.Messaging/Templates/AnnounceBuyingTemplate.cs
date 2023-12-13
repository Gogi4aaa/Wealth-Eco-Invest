namespace Wealth_Eco_Invest.Services.Messaging.Templates
{
	using System.Text.Encodings.Web;

	public class AnnounceBuyingTemplate
	{
		public static string Message(string userName, string callbackUrl)
		{
			return @$"
			<div style='text-align: center;'>
		        <div>
		            <h2>Thanks for your participation and welcome to the community!</h2>
		            <p>Hi {userName},</p>
		            <p>
					thanks for your buying! We appreciate that because you help to our community and
				save the environment!
					</p>
		            <a style='padding: 16px 20px;background: orange;color: white;border-radius: 3px;text-decoration: none;font-size: 1.2rem;' href='{HtmlEncoder.Default.Encode(callbackUrl)}' target='_blank'>Go back to store</a>
		        </div>
			</div>";
		}
	}
}
