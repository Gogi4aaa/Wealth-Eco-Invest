namespace Wealth_Eco_Invest.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(SeedCategories());
        }

        private Category[] SeedCategories()
        {
            //TODO add categories Name

            ICollection<Category> categories = new HashSet<Category>();

            Category category;

            category = new Category()
            {
                Id = 1,
                Name = "Залесяване"
			};

            categories.Add(category);

            category = new Category()
            {
                Id = 2,
                Name = "Рециклиране"
			};
            categories.Add(category);

            category = new Category()
            {
                Id = 3,
                Name = "Почистване"
			};
            categories.Add(category);

            return categories.ToArray();
        }
    }
}
