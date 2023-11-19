using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Wealth_Eco_Invest.Data
{
	using Microsoft.AspNetCore.Identity;

	public class ApplicationDbContext : IdentityDbContext<ApplicationUser,IdentityRole<Guid>, Guid>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
	}
}