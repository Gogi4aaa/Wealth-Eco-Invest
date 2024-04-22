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
			

			return chats;
		}

		public async Task<Guid> GetLatestForumIdAsync(Guid userId)
		{
			var chats = await this.dbContext
				.Chats
				.Where(x => (x.UserTo == null))
				.ToListAsync();

			var allPossibleForums = await this.dbContext
				.Purchases
				.Where(x => x.BuyerId == userId)
				.Select(x => x.AnnounceId)
				.ToListAsync();

			var allFinal = new List<Chat>();
			foreach (var guid in allPossibleForums)
			{
				foreach (var chat1 in chats)
				{
					if (guid == chat1.AnnounceId)
					{
						allFinal.Add(chat1);
					}
				}
			}

			var latestForum = allFinal
				.OrderBy(x => x.StartedOn)
				.LastOrDefault();

			return latestForum.Id;
		}
	}
}
