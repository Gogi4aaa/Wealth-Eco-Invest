namespace Wealth_Eco_Invest.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Newtonsoft.Json;

	using System.Diagnostics;
	using System.Net.Http;

	using Models;
	using NewsAPI;
	using NewsAPI.Constants;
	using NewsAPI.Models;
	using Services.Data.Models.News;

	using static Common.GeneralApplicationConstants;
	using static Common.NotificationMessagesConstants;
	
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly HttpClient _client;
		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
			_client = new HttpClient();
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var articlesResponse = new ArticlesResult();
			try
			{
				var newsApiClient = new NewsApiClient("8a9b459dee964c2291e961c51aa7c822");
				articlesResponse = await newsApiClient.GetEverythingAsync(new EverythingRequest
				{
					Q = "environment",
					SortBy = SortBys.Popularity,
					Language = Languages.EN,
					PageSize = 8,
				});

			}
			catch (Exception)
			{
				TempData[ErrorMessage] = "Unexpected message occurred";
			}
			
			return View(articlesResponse);
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}