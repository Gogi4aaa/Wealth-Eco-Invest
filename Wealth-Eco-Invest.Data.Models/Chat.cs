namespace Wealth_Eco_Invest.Data.Models
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Chat
	{
		[Key]
		public Guid Id { get; set; }

		public Guid UserFrom { get; set; }

		public Guid UserTo { get; set; }

		public string Message { get; set; }

		public DateTime TypedOn { get; set; }

		[ForeignKey(nameof(Announce))]
		public Guid AnnounceId { get; set; }

		public Announce Announce { get; set; }

	}
}
