namespace Wealth_Eco_Invest.WebApi.Controllers
{
	using System.ComponentModel.DataAnnotations;
	using Microsoft.AspNetCore.Mvc;
	using Services.Data.Models.News;
	using static Common.GeneralApplicationConstants;
	
	[ApiController]
	public class EnvironmentNewsController : ControllerBase
	{
		[HttpGet]
		[Produces("application/json")]
		[ProducesResponseType(200, Type = typeof(EnvironmentNewsServiceModel))]
		[ProducesResponseType(400)]
		public IActionResult Index()
		{

			return Ok();
		}
	}
}
