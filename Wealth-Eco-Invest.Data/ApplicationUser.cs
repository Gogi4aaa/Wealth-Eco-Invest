namespace Wealth_Eco_Invest.Data
{
	using System.ComponentModel.DataAnnotations;
	using Microsoft.AspNetCore.Identity;
	
	using static Common.ValidationConstants.User;

	public class ApplicationUser : IdentityUser<Guid>
	{
		public ApplicationUser()
		{
			this.Id = Guid.NewGuid();
        }

	}
}
