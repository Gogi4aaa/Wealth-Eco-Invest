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
		public CalendarController(IShoppingCartService shoppingCartService)
		{
			this.shoppingCartService = shoppingCartService;
		}
		public async Task<IActionResult> EventCalendar()
		{
			var elements = await this.shoppingCartService.GetAllAnnouncesForUser(Guid.Parse(this.User.GetId()!));
			List<CalendarViewModel> allElements = new List<CalendarViewModel>();
			foreach (var element in elements.Announces)
			{
				CalendarViewModel calendar = new CalendarViewModel()
				{
					Title = element.Title,
					Start = DateTime.Now
				};
				allElements.Add(calendar);
			}

			return View(allElements.ToArray());
		}
	}
}
