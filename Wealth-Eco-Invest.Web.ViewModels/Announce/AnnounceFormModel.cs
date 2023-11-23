namespace Wealth_Eco_Invest.Web.ViewModels.Announce
{
	using System.ComponentModel.DataAnnotations;
	using Category;
	using static Common.ValidationConstants.Announce;
	using static Common.ErrorMessages.Announce;
	public class AnnounceFormModel
	{
		public AnnounceFormModel()
		{
			this.Categories = new HashSet<AnnounceSelectCategoryFormModel>();
		}

		[Required]
		[StringLength(TitleMaxLength, MinimumLength = TitleMinLength, ErrorMessage = TitleErrorMessage)]
		public string Title { get; set; } = null!;

		[Required]
		[StringLength(DescriptionMaxLength,MinimumLength = DescriptionMinLength,ErrorMessage = DescriptionErrorMessage)]
		public string Description { get; set; } = null!;

		[Display(Name = "Image Link")]
		public string ImageUrl { get; set; } = null!;

		[Range(PriceMinLength,PriceMaxLength)]
		public decimal Price { get; set; }

		[Display(Name = "Category")]
		public int CategoryId { get; set; }

		public Guid UserId { get; set; }
		public IEnumerable<AnnounceSelectCategoryFormModel> Categories { get; set; }
	}
}
