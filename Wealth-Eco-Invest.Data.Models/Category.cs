namespace Wealth_Eco_Invest.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Common.ValidationConstants.Category;

    public class Category
    {
        public Category()
        {
            Announces = new HashSet<Announce>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public ICollection<Announce> Announces { get; set; }
    }
}
