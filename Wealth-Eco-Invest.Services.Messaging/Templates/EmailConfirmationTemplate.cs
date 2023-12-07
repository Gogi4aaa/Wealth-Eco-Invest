using System.Text.Encodings.Web;

namespace Wealth_Eco_Invest.Services.Messaging.Templates
{
	public class EmailConfirmationTemplate
	{
		public static string Message(string callbackUrl, string userName)
		{
			return @$"
			<div style='text-align: center;'>
				<h2>Account confirmation</h2>
				<p>Hello {userName} you tried to login in our website.</p>
				<p>
					To continue confirm your account by clicking here: <a			href='{HtmlEncoder.Default.Encode(callbackUrl)}' target='_blank' >Confirm	account</a>.
				</p>
			</div>";
		}
	}
}
