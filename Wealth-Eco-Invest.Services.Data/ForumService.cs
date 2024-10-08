﻿namespace Wealth_Eco_Invest.Services.Data
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
			List<AllChatsViewModel> chats = new List<AllChatsViewModel>();

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

			foreach (var ownedForums in userOwnerOfAnnounce)
			{
				chats.Add(ownedForums);
			}

			var announcesIdsOfUser = await this.dbContext.Purchases
				.Where(x => x.BuyerId == userId)
				.Select(x => x.AnnounceId)
				.ToListAsync();
			
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
					var isExist = chats.Any(x => x.ChatId == forum[0].ChatId);
					if (!isExist)
					{
						chats.Add(forum[0]);
					}
				}
			}

			return chats.OrderByDescending(x => x.StartedOn).ToList();
		}
	}
}
