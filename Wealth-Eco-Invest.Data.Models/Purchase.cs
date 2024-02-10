namespace Wealth_Eco_Invest.Data.Models
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Purchase
	{
		public Purchase()
		{
			this.Id = Guid.NewGuid();
		}

		[Key]
		public Guid Id { get; set; }

		[ForeignKey(nameof(Announce))]
		public Guid AnnounceId { get; set; }

		public Announce Announce { get; set; }

		public Guid BuyerId { get; set; }
	}
}
