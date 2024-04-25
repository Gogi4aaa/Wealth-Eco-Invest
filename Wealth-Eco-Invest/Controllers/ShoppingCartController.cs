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
	using static Common.GeneralApplicationConstants;
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
		private readonly INotificationService notificationService;

		public ShoppingCartController(IShoppingCartService shoppingCartService, IAnnounceService announceService, IEmailSender emailSender, IPurchaseService purchaseService, INotificationService notificationService)
        {
            this.shoppingCartService = shoppingCartService;
            this.announceService = announceService;
			this.emailSender = emailSender;
			this.purchaseService = purchaseService;
			this.notificationService = notificationService;
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
	            var IsAnnounceOwnedByUser = await 
		            this.announceService.IsCurrentUserOwnedAnnounceAsync(id, Guid.Parse(this.User.GetId()!));
	            if (IsAnnounceOwnedByUser)
	            {
		            TempData[WarningMessage] = "Не можете да закупувате билети за събития създадени от вас!";
		            return RedirectToAction("All", "Announce");
				}
				bool isAnnounceAlreadyBoughtByUser =
		            await this.purchaseService.CheckIsThisAnnounceIsAlreadyBoughtByCurrentUser(id,
			            Guid.Parse(this.User.GetId()!));
	            if (isAnnounceAlreadyBoughtByUser)
	            {
		            TempData[WarningMessage] = "Вече сте регистриран за тази обява!";
					return RedirectToAction("All", "Announce");
				}
                await this.shoppingCartService.AddAnnounceToUser(id, Guid.Parse(this.User.GetId()!));
                
                TempData[SuccessMessage] = "Обявата беше добавена в количката";
            }
            catch (Exception e)
            {
                TempData[ErrorMessage] = CommonErrorMessage;
            }

            return RedirectToAction("All", "ShoppingCart");
        }

        public async Task<IActionResult> Remove(Guid id)
        {
	        try
	        {
				await this.shoppingCartService.DeleteAnnounceToUser(id, Guid.Parse(this.User.GetId()!));
				TempData[SuccessMessage] = "Успешно премахнахте обявата от количката си!";
	        }
	        catch (Exception e)
	        {
		        TempData[ErrorMessage] = CommonErrorMessage;
	        }
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
		        TempData[ErrorMessage] = "Минималният брой е 1!";
	        }
	        else
	        {
				await this.shoppingCartService.UpdateAnnounceToUser(announce.Id,Guid.Parse(this.User.GetId()!), announce.Count);
	        }
	        
			return RedirectToAction("All", "ShoppingCart");
		}

        public async Task<IActionResult> Buy()
        {
	        ShoppingCartViewModel announces = await this.shoppingCartService.GetAllAnnouncesForUser(Guid.Parse(this.User.GetId()!));

	        List<SessionLineItemOptions> sessionList = new List<SessionLineItemOptions>();
	        foreach (var announce in announces.Announces)
	        {
				var stringPrice = announce.Price.ToString();
				if (stringPrice.Contains("."))
				{
					stringPrice = stringPrice.Replace(".", "");
				}
				else if (stringPrice.Contains(","))
				{
					stringPrice = stringPrice.Replace(",", "");
				}
				var price = decimal.Parse(stringPrice);

				var sessionItem = new SessionLineItemOptions()
				{
					PriceData = new SessionLineItemPriceDataOptions()
					{
						UnitAmountDecimal = price,
						Currency = "BGN",
						ProductData = new SessionLineItemPriceDataProductDataOptions
						{
							Name = announce.Title,
							Images = new List<string> { announce.ImageUrl }
						}
					},
					Quantity = announce.Count,

				};
				sessionList.Add(sessionItem);
			}

			var location = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}");

			var url = "https://";
				url += location.Authority;

	        var options = new SessionCreateOptions
			{
				SuccessUrl = $"{url}/ShoppingCart/Success",
				CancelUrl = $"{url}/ShoppingCart/Failed",
				PaymentMethodTypes = new List<string> 
				{
					"card"
				},
				LineItems = sessionList,
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
				TempData[ErrorMessage] = CommonErrorMessage;
				return View("Failed");

			}

			return Redirect(session.Url);
        }

        public async Task<IActionResult> Success()
        {
	        var callbackUrl = Url.Page(
				"/",
				pageHandler: null,
				values: new { area = "" },
				protocol: Request.Scheme);

			try
			{
				var allPurchasedAnnounce = await
					this.shoppingCartService.GetAllAnnouncesForUser(Guid.Parse(this.User.GetId()!));
				bool isAlreadyBought = false;
				Guid alredyBoughtAnnounceId = new Guid();
				foreach (var purchasedAnnounce in allPurchasedAnnounce.Announces)
				{
					bool isAnnounceAlreadyBoughtByUser =
			        await this.purchaseService.CheckIsThisAnnounceIsAlreadyBoughtByCurrentUser(purchasedAnnounce.Id,Guid.Parse(this.User.GetId()!));
					if (isAnnounceAlreadyBoughtByUser)
					{
						isAlreadyBought = true;
						alredyBoughtAnnounceId = purchasedAnnounce.Id;
						break;
					}
				}
		       
		        if (isAlreadyBought)
		        {
			        var boughtAnnounce = await 
				        this.shoppingCartService.GetAnnounceByAnnounceId(alredyBoughtAnnounceId,
					        Guid.Parse(this.User.GetId()!));

			        TempData[ErrorMessage] = $"Вие вече участвате в {boughtAnnounce.Title}";
					TempData[InformationMessage] = "Проверете календара си!";

			        return View();
		        }

		        foreach (var purchasedAnnounce in allPurchasedAnnounce.Announces)
				{
					var announceForTimer = await this.shoppingCartService.GetAnnounceByAnnounceId(purchasedAnnounce.Id, Guid.Parse(this.User.GetId()!));

					await this.notificationService.SendEmailNotification(announceForTimer, Guid.Parse(this.User.GetId()!));

					await this.purchaseService.PurchaseAnnounceAsync(purchasedAnnounce.Id, Guid.Parse(this.User.GetId()!));

					await this.shoppingCartService.DeleteAnnounceToUser(purchasedAnnounce.Id, Guid.Parse(this.User.GetId()!));
				}

				TempData[InformationMessage] = "Проверете календара си за цялата информация!";
			}
	        catch (Exception)
	        {
		        TempData[ErrorMessage] = CommonErrorMessage;
			}

	        return View();
        }

        public IActionResult Failed()
        {
	        return View();
		}
	}
	
}
