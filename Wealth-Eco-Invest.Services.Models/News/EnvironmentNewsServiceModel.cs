using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wealth_Eco_Invest.Services.Models.News
{
	using NewsAPI.Models;
	using Common;
	public class EnvironmentNewsServiceModel : ArticlesResult
	{
		public EnvironmentNewsServiceModel()
		{
			TotalNews = GeneralApplicationConstants.TotalNews;
			NewsPerPage = GeneralApplicationConstants.NewsPerPage;
			CurrentPage = 1;
		}
		public int CurrentPage { get; set; }

		public int TotalNews { get; set; }

		public int NewsPerPage { get; set; }
	}
}
