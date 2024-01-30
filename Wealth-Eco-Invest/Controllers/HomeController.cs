using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Wealth_Eco_Invest.Models;

namespace Wealth_Eco_Invest.Controllers
{
	using static Common.GeneralApplicationConstants;
	using static Common.NotificationMessagesConstants;
	using NuGet.Protocol;
	using System.Net.Http;
	using System.Text.Json;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;
	using Services.Data.Models.News;
	using JsonSerializer = System.Text.Json.JsonSerializer;
	using static System.Net.WebRequestMethods;
	using Wealth_Eco_Invest.Common;

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