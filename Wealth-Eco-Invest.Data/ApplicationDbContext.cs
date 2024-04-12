namespace Wealth_Eco_Invest.Data
{
    using Configurations;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
	
    using Microsoft.AspNetCore.Identity;
    using Models;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,IdentityRole<Guid>, Guid>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Announce> Announces { get; set; } = null!;
        public DbSet<Cart> Carts { get; set; } = null!;
        public DbSet<Purchase> Purchases { get; set; } = null!;
        public DbSet<Chat> Chats { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AnnounceEntityConfiguration());
            builder.ApplyConfiguration(new CategoryEntityConfiguration());
            builder.ApplyConfiguration(new SeedAnnouncesEntityConfiguration());

            base.OnModelCreating(builder);
        }
    }
}