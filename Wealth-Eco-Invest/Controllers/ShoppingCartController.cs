using Microsoft.AspNetCore.Mvc;

namespace Wealth_Eco_Invest.Controllers
{
    using Data.Models;
    using Services.Data.Interfaces;
    using Common;
    using Microsoft.AspNetCore.Authorization;
    using Web.Infrastructure.Extensions;
    using static Common.NotificationMessagesConstants;
    using NuGet.Packaging;
    using Web.ViewModels.Announce;
    using Web.ViewModels.ShoppingCart;

    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService shoppingCartService;
        private readonly IAnnounceService announceService;

        public ShoppingCartController(IShoppingCartService shoppingCartService, IAnnounceService announceService)
        {
            this.shoppingCartService = shoppingCartService;
            this.announceService = announceService;
        }

        public async Task<IActionResult> All()
        {
	        ShoppingCartViewModel all = await this.shoppingCartService.GetAllAnnouncesForUser(Guid.Parse(this.User.GetId()!));

            return View(all);
        }

        public async Task<IActionResult> Add(Guid id)
        {
            try
            {
                await this.shoppingCartService.AddAnnounceToUser(id, Guid.Parse(this.User.GetId()!));
                
                TempData[SuccessMessage] = "Announce was added to cart!";
                TempData[InformationMessage] = "Check your calendar for more info!";
            }
            catch (Exception e)
            {
                TempData[ErrorMessage] = "Unexpected message occurred";
            }

            

            return RedirectToAction("All", "ShoppingCart");
        }

        [HttpGet]
        public async Task<IActionResult> IncreaseCount(Guid id)
        {
	        var announce = await this.announceService.GetAnnounceByAnnounceIdForShoppingCart(id,Guid.Parse(this.User.GetId()!));
	        announce.Count += 1;
	        await this.shoppingCartService.UpdateAnnounceToUser(announce.Id, Guid.Parse(this.User.GetId()!),announce.Count);
	        return RedirectToAction("All", "ShoppingCart");
		}

        [HttpGet]
        public async Task<IActionResult> DecreaseCount(Guid id)
        {
	        var announce = await this.announceService.GetAnnounceByAnnounceIdForShoppingCart(id, Guid.Parse(this.User.GetId()!));
	        announce.Count-= 1;
	        if (announce.Count <= 0)
	        {
		        TempData[ErrorMessage] = "The minimum quantity is 1!";
	        }
	        else
	        {
				await this.shoppingCartService.UpdateAnnounceToUser(announce.Id,Guid.Parse(this.User.GetId()!), announce.Count);
	        }
	        
			return RedirectToAction("All", "ShoppingCart");
		}
	}
}
