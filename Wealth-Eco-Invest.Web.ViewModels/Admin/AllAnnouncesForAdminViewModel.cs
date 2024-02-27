namespace Wealth_Eco_Invest.Web.ViewModels.Admin
{
	using System.ComponentModel.DataAnnotations;
	public class AllAnnouncesForAdminViewModel
	{
		public Guid Id { get; set; }

		public Guid UserId { get; set; }

		public string Username { get; set; } = null!;

		public string Email { get; set; } = null!;

		public string Title { get; set; } = null!;

		public string Category { get; set; } = null!;

		public bool IsActive { get; set; }

		public string Description { get; set; } = null!;

		public DateTime StartDate { get; set; }

		public DateTime CreatedOn { get; set; }

		[Display(Name = "Image Link")]
		public string ImageUrl { get; set; } = null!;

		public decimal Price { get; set; }
	}
}