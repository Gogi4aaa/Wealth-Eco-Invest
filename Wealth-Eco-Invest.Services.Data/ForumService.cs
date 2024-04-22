namespace Wealth_Eco_Invest.Services.Data
{
	using Interfaces;
	using Microsoft.EntityFrameworkCore;
	using Wealth_Eco_Invest.Data;
	using Wealth_Eco_Invest.Data.Models;
	using Web.ViewModels.Messages;
	using static Wealth_Eco_Invest.Common.ValidationConstants;

	public class ForumService : IForumService
	{
		private readonly ApplicationDbContext dbContext;

		public ForumService(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		public async Task<List<AllChatsViewModel>> GetAllForumsByUserIdAsync(Guid userId)
		{
			var announcesIdsOfUser = await this.dbContext.Purchases
				.Where(x => x.BuyerId == userId)
				.Select(x => x.AnnounceId)
				.ToListAsync();
			List<AllChatsViewModel> chats = new List<AllChatsViewModel>();
			foreach (var announceId in announcesIdsOfUser)
			{
				var forum = await this.dbContext
					.Chats
					.Where(x => x.AnnounceId == announceId && x.UserTo == null)
					.Select(x => new AllChatsViewModel()
					{
						UserTo = x.UserTo,
						UserFrom = x.UserFrom,
						StartedOn = x.StartedOn,
						ChatId = x.Id,
						AnnounceName = x.Announce.Title,
					})
					.ToListAsync();//suspicious
				if (forum.Any())
				{
					chats.Add(forum[0]);
				}
			}

			var userOwnerOfAnnounce = await this.dbContext
				.Chats
				.Where(x => x.UserFrom == userId && x.UserTo == null)
				.Select(x => new AllChatsViewModel()
				{
					UserTo = x.UserTo,
					UserFrom = x.UserFrom,
					StartedOn = x.StartedOn,
					ChatId = x.Id,
					AnnounceName = x.Announce.Title,
				})
				.ToListAsync();

			if (userOwnerOfAnnounce.Any())
			{
				chats.Add(userOwnerOfAnnounce[0]);
			}
			

			return chats.OrderByDescending(x => x.StartedOn).ToList();
		}
	}
}
