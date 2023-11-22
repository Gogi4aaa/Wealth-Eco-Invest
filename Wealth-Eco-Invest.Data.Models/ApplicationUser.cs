namespace Wealth_Eco_Invest.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using static Common.ValidationConstants.User;
    public class ApplicationUser : IdentityUser<Guid>
	{
		public ApplicationUser()
		{
			Id = Guid.NewGuid();
            Announces = new HashSet<Announce>();
        }

        public ICollection<Announce> Announces { get; set; }

	}
}
