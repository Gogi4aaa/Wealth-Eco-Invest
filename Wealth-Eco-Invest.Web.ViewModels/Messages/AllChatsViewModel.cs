﻿namespace Wealth_Eco_Invest.Web.ViewModels.Messages
{
	public class AllChatsViewModel
	{
		public string Name { get; set; }

		public DateTime StartedOn { get; set; }

		public Guid? UserTo { get; set; }

		public Guid UserFrom { get; set; }

		public Guid ChatId { get; set; }

		public string Message { get; set; }

		public string AnnounceName { get; set; }
	}
}
