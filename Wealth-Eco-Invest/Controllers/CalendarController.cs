using Microsoft.AspNetCore.Mvc;

namespace Wealth_Eco_Invest.Controllers
{
	using Microsoft.AspNetCore.Authorization;
	using Services.Data.Interfaces;
	using Web.Infrastructure.Extensions;
	using Web.ViewModels.Calendar;

	[Authorize]
	public class CalendarController : Controller
	{
		private readonly IShoppingCartService shoppingCartService;
		private readonly IPurchaseService purchaseService;
		public CalendarController(IShoppingCartService shoppingCartService, IPurchaseService purchaseService)
		{
			this.shoppingCartService = shoppingCartService;
			this.purchaseService = purchaseService;
		}
		public IActionResult EventCalendar()
		{
			return View();
		}

		[HttpGet]
		public async Task<JsonResult> GetAll()
		{
			var elements = await this.purchaseService.GetAllPurchasedAnnouncesByUserIdAsync(Guid.Parse(this.User.GetId()!));
			List<CalendarViewModel> allElements = new List<CalendarViewModel>();
			foreach (var element in elements.Announces)
			{
				CalendarViewModel calendar = new CalendarViewModel()
				{
					Title = element.Title,
					Start = element.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
			};
				allElements.Add(calendar);
			}
			return Json(allElements.ToArray());
		}


		//public async Task<IActionResult> DeleteEvent(Guid id)
		//{
		//	this.shoppingCartService.DeleteAnnounceToUser()
		//}
	}
}