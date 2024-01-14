namespace Wealth_Eco_Invest.Web.ViewModels.Announce
{
    using System.ComponentModel.DataAnnotations;
    using static Common.ValidationConstants.Announce;
    using static Common.ErrorMessages.Announce;
    public class AnnounceDetailsViewModel
    {
	    public AnnounceDetailsViewModel()
	    {
		    Id = Guid.NewGuid();
	    }
        
	    public Guid Id { get; set; }

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength, ErrorMessage = TitleErrorMessage)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = DescriptionErrorMessage)]
        public string Description { get; set; } = null!;

        [Required]
        [Range(PriceMinLength, PriceMaxLength, ErrorMessage = PriceErrorMessage)]
        public decimal Price { get; set; }

        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public string Owner { get; set; } = null!;

    }
}
