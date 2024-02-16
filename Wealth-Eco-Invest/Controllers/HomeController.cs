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
		public async Task<IActionResult> Index(int page = 1)
		{
			var returnResponse = new EnvironmentNewsServiceModel();
			try
			{
				var newsApiClient = new NewsApiClient("8a9b459dee964c2291e961c51aa7c822");
				var articlesResponse = await newsApiClient.GetEverythingAsync(new EverythingRequest
				{
					Q = "environment",
					SortBy = SortBys.PublishedAt,
					Language = Languages.EN,
					PageSize = 8,
					Page = page,
				});

				returnResponse.Articles = articlesResponse.Articles;
				returnResponse.CurrentPage = page;
			}
			catch (Exception)
			{
				TempData[ErrorMessage] = "Unexpected message occurred";
			}
			
			return View(returnResponse);
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}