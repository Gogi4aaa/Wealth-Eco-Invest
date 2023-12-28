using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wealth_Eco_Invest.Data.Models
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Cart
	{
		public Cart()
		{
			this.Id = Guid.NewGuid();
		}
		[Key]
		public Guid Id { get; set; }

		[ForeignKey(nameof(Announce))]
		public Guid AnnounceId { get; set; }

		public Announce Announce { get; set; }

		public Guid BuyerId { get; set; }

		public int Quantity { get; set; }
	}
}
