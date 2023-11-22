namespace Wealth_Eco_Invest.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class AnnounceEntityConfiguration : IEntityTypeConfiguration<Announce>
    {
        public void Configure(EntityTypeBuilder<Announce> builder)
        {
            builder
                .Property(x => x.CreatedOn)
                .HasDefaultValueSql("GETDATE()");

            builder
                .Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder
                .HasOne(u => u.User)
                .WithMany(a => a.Announces)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder
                .HasOne(c => c.Category)
                .WithMany(a => a.Announces)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
