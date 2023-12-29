namespace Wealth_Eco_Invest.Web.ViewModels.Announce
{
    using System.ComponentModel.DataAnnotations;

    public class AllAnnouncesViewModel
    {
	    public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime CreatedOn { get; set; }

        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;

        public decimal Price { get; set; }

        public int Count { get; set; }

    }
}
