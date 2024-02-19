namespace Wealth_Eco_Invest.Controllers
{
	using Microsoft.AspNetCore.Mvc;

    using Data.Models;
    using Services.Data.Interfaces;
    using Common;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.JSInterop;
    using Newtonsoft.Json;
    using Web.Infrastructure.Extensions;
    using static Common.NotificationMessagesConstants;
    using NuGet.Packaging;
	using Services.Messaging;
	using Services.Messaging.Templates;
    using Web.ViewModels.Announce;
    using Web.ViewModels.ShoppingCart;
    using Stripe.Checkout;

	[Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService shoppingCartService;
        private readonly IAnnounceService announceService;
        private readonly IEmailSender emailSender;
		private readonly IPurchaseService purchaseService;

		public ShoppingCartController(IShoppingCartService shoppingCartService, IAnnounceService announceService, IEmailSender emailSender, IPurchaseService purchaseService)
        {
            this.shoppingCartService = shoppingCartService;
            this.announceService = announceService;
			this.emailSender = emailSender;
			this.purchaseService = purchaseService;
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
	            bool isAnnounceAlreadyBoughtByUser =
		            await this.purchaseService.CheckIsThisAnnounceIsAlreadyBoughtByCurrentUser(id,
			            Guid.Parse(this.User.GetId()!));
	            if (isAnnounceAlreadyBoughtByUser)
	            {
		            TempData[WarningMessage] = "You are already registered for this announce!";
					return RedirectToAction("All", "Announce");
				}
                await this.shoppingCartService.AddAnnounceToUser(id, Guid.Parse(this.User.GetId()!));
                
                TempData[SuccessMessage] = "Announce was added to cart!";
            }
            catch (Exception e)
            {
                TempData[ErrorMessage] = "Unexpected exception occurred";
            }

            return RedirectToAction("All", "ShoppingCart");
        }

        public async Task<IActionResult> Remove(Guid id)
        {
	        await this.shoppingCartService.DeleteAnnounceToUser(id, Guid.Parse(this.User.GetId()!));
	        return RedirectToAction("All", "ShoppingCart");
        }

        [HttpGet]
        public async Task<IActionResult> IncreaseCount(Guid id)
        {
	        var announce = await this.shoppingCartService.GetAnnounceByAnnounceId(id,Guid.Parse(this.User.GetId()!));
	        announce.Count += 1;
	        await this.shoppingCartService.UpdateAnnounceToUser(announce.Id, Guid.Parse(this.User.GetId()!),announce.Count);
	        return RedirectToAction("All", "ShoppingCart");
		}

        [HttpGet]
        public async Task<IActionResult> DecreaseCount(Guid id)
        {
	        var announce = await this.shoppingCartService.GetAnnounceByAnnounceId(id, Guid.Parse(this.User.GetId()!));
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

        public async Task<IActionResult> Buy(Guid id)
        {
	        AllAnnouncesViewModel announce = await this.shoppingCartService.GetAnnounceByAnnounceId(id, Guid.Parse(this.User.GetId()!));

			var location = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}");

			var url = "https://";
				url += location.Authority;

	        var options = new SessionCreateOptions
			{
				SuccessUrl = $"{url}/ShoppingCart/Success/{announce.Id}",
				CancelUrl = $"{url}/ShoppingCart/Failed",
				PaymentMethodTypes = new List<string> 
				{
					"card"
				},
				LineItems = new List<SessionLineItemOptions>
				{
					new SessionLineItemOptions
					{
						PriceData = new SessionLineItemPriceDataOptions
						{
							UnitAmountDecimal = decimal.Parse(announce.Price.ToString().Replace(".", "")),
							Currency = "BGN",
							ProductData = new SessionLineItemPriceDataProductDataOptions
							{
								Name = announce.Title,
								Description = announce.Description,
								Images = new List<string> { announce.ImageUrl }
							}
						},
						Quantity = announce.Count,
					},
				},
				Mode = "payment",
				CustomerEmail = this.User.GetEmail(),
			};

			var service = new SessionService();
			Session session = null;
			try
			{
				session = await service.CreateAsync(options);
			}
			catch (Exception)
			{
				TempData[ErrorMessage] = "Unexpected exception occurred";
				return View("Failed");

			}

			return Redirect(session.Url);
        }

        public async Task<IActionResult> Success(Guid id)
        {
	        var callbackUrl = Url.Page(
				"/",
				pageHandler: null,
				values: new { area = "" },
				protocol: Request.Scheme);

	        try
	        {
		        bool isAnnounceAlreadyBoughtByUser =
			        await this.purchaseService.CheckIsThisAnnounceIsAlreadyBoughtByCurrentUser(id, Guid.Parse(this.User.GetId()!));
		        if (isAnnounceAlreadyBoughtByUser)
		        {
			        TempData[ErrorMessage] = "You have already participate in this announce!";
					TempData[InformationMessage] = "Check your calendar!";

			        return View();
		        }
		        await this.purchaseService.PurchaseAnnounceAsync(id, Guid.Parse(this.User.GetId()!));

		        var announce = await this.shoppingCartService.GetAnnounceByAnnounceId(id, Guid.Parse(this.User.GetId()!));

				await this.emailSender.SendEmailAsync(this.User.GetEmail()!, "Announce buying", AnnounceBuyingTemplate.Message(this.User.Identity!.Name!, callbackUrl, announce));

				await this.shoppingCartService.DeleteAnnounceToUser(id, Guid.Parse(this.User.GetId()!));

				TempData[InformationMessage] = "Check your calendar for full information!";
			}
	        catch (Exception)
	        {
		        TempData[ErrorMessage] = TempData[ErrorMessage] = "Unexpected error occurred";
			}

	        return View();
        }

        public IActionResult Failed()
        {
	        return View();
		}
	}
	
}
