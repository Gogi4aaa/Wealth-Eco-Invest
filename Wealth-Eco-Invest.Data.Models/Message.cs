namespace Wealth_Eco_Invest.Data.Models
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Message
	{
		[Key]
		public Guid Id { get; set; }

		[ForeignKey(nameof(Chat))]
		public Guid ChatId { get; set; }
		public Chat Chat { get; set; }

		public string Content { get; set; }

		public DateTime TypedOn { get; set; }

		public string OwnerUsername { get; set; } = null!;
	}
}
