namespace Wealth_Eco_Invest.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class SeedAnnouncesEntityConfiguration : IEntityTypeConfiguration<Announce>
    {
        public void Configure(EntityTypeBuilder<Announce> builder)
        {
            builder.HasData(SeedAnnounces());
        }

        private Announce[] SeedAnnounces()
        {
            ICollection<Announce> announces = new HashSet<Announce>();

            Announce announce;

            announce = new Announce()
            {
                Title = "Air pollution",
                Description = "This rubbish pollutes the environment.You need to throw it in the trash bins!",
                Price = 50.00m,
                StartDate = DateTime.Parse("2023-12-01 12:25:00"),
                ImageUrl = "https://www.nrdc.org/sites/default/files/styles/medium_16x9_100/public/media-uploads/health4_26_airpollguide_istock_2796602_2400.jpg.jpg?h=c3635fa2&itok=bunvf3B8",
                CategoryId = 3,
                UserId = Guid.Parse("5168C799-18F9-4C11-AC0A-3F0B210E742D"),
            };
            announces.Add(announce);

            announce = new Announce()
            {
                Title = "Clean the rubbish",
                Description = "The pollution is the worst thing ever.We need to stop it!",
                Price = 200.00m,
                StartDate = DateTime.Parse("2023-12-02 10:30:00"),
				ImageUrl = "https://www.interplas.com/product_images/trash-bags/sku/8-10-Gallon-Black-24-x-30-Drawstring-Trash-Bags-1000px.jpg?v=1346367336",
                CategoryId = 1,
                UserId = Guid.Parse("4F2762FC-4689-4FBD-8E55-8E84EAC060CD"),
            };
            announces.Add(announce);

            announce = new Announce()
            {
                Title = "Water pollution",
                Description = "Water pollution destroy our beaches and oceans.We need to stop it fast!",
                Price = 100_000m,
                StartDate = DateTime.Parse("2023-12-01 15:05:05"),
				ImageUrl =
                    "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRdMLiNp09BNzW4jIdGFBpJx4yDwVfeXkygeQ&usqp=CAU",
                CategoryId = 2,
                UserId = Guid.Parse("3A9C9BEA-9F47-4924-AF37-5079AAD72902"),
            };
            announces.Add(announce);

            return announces.ToArray();
        }
    }
}
