namespace Wealth_Eco_Invest.Web.ViewModels.Announce
{
	using System.ComponentModel.DataAnnotations;
	using Microsoft.AspNetCore.Http;
	using Category;
	using Infrastructure.CustomValidationAttributes;
	using static Common.ValidationConstants.Announce;
	using static Common.ErrorMessages.Announce;
	public class AnnounceFormModel
	{
		public AnnounceFormModel()
		{
			this.Categories = new HashSet<AnnounceSelectCategoryFormModel>();
		}

		[Display(Name = "Име")]
		[Required]
		[StringLength(TitleMaxLength, MinimumLength = TitleMinLength, ErrorMessage = TitleErrorMessage)]
		public string Title { get; set; } = null!;

		[Display(Name = "Описание")]
		[Required]
		[StringLength(DescriptionMaxLength,MinimumLength = DescriptionMinLength,ErrorMessage = DescriptionErrorMessage)]
		public string Description { get; set; } = null!;

		public IFormFile ProductImage { get; set; }

		[Display(Name = "Снимка")]
		public string ImageUrl { get; set; } = null!;

		[Display(Name = "Цена")]
		[Range(PriceMinLength,PriceMaxLength)]
        public decimal Price { get; set; }

		[MyDate(ErrorMessage = "Невалидна дата")]
		[Display(Name = "Дата на започване")]
		public DateTime StartDate { get; set; }

        public DateTime OldDate { get; set; }

		[Display(Name = "Категория")]
		public int CategoryId { get; set; }

		public Guid UserId { get; set; }
		public IEnumerable<AnnounceSelectCategoryFormModel> Categories { get; set; }
	}
}
