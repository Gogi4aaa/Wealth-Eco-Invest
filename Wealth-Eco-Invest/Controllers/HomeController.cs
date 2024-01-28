namespace Wealth_Eco_Invest.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Newtonsoft.Json;

	using System.Diagnostics;
	using System.Net.Http;

	using Models;
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
			EnvironmentNewsServiceModel.Root myDeserializedClass = new EnvironmentNewsServiceModel.Root();
			try
			{
				var response = await _client.GetAsync(NewsUrl);
				var responseContent = await response.Content.ReadAsStringAsync();
				 myDeserializedClass = JsonConvert.DeserializeObject<EnvironmentNewsServiceModel.Root>(responseContent)!;

			}
			catch (Exception)
			{
				TempData[ErrorMessage] = "Unexpected message occurred";
			}
			
			return View(myDeserializedClass);
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}