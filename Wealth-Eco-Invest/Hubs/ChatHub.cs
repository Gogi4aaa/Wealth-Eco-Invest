namespace Wealth_Eco_Invest.Hubs;

using Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Web.Infrastructure.Extensions;

public class ChatHub : Hub
{
	private readonly ApplicationDbContext dbContext;

	public ChatHub(ApplicationDbContext dbContext)
	{
		this.dbContext = dbContext;
	}

	public override async Task OnConnectedAsync()
	{
		var user = await dbContext.Users.FindAsync(GetUserId());
		user.ConnectionId = GetConnectionId();
		await dbContext.SaveChangesAsync();
		await AddUserToGroups();
		//dobavqm connection-Idto kum momentniq potrebitel v kolonkata
		await base.OnConnectedAsync();
	}

	public override async Task OnDisconnectedAsync(Exception? exception)
	{
		var user = await dbContext.Users.FindAsync(GetUserId());
		user.ConnectionId = null;
		await dbContext.SaveChangesAsync();
		//premahvam connection-Idto kum momentniq potrebitel ot kolonkata
		await base.OnDisconnectedAsync(exception);
	}

	private async Task AddUserToGroups()
	{
		var userId = GetUserId();
		var chatIdsOfUser = await dbContext.Chats
			.Where(x => x.UserFrom == userId && x.UserTo != null || x.UserTo == userId)
			.Select(x => x.Id)
			.ToListAsync();

		var announces = await dbContext.Announces
			.Where(x => x.UserId == userId)
			.ToListAsync();

		var announceIdsOrUser = await dbContext.Purchases
			.Where(x => x.BuyerId == userId)
			.Select(x => x.AnnounceId)
			.ToListAsync();

		var chats = await this.dbContext
			.Chats
			.Where(x => x.UserFrom == userId && x.UserTo == null || x.UserTo == null)
			.ToListAsync();

		var correctChats = new List<Guid>();
		foreach (var ch in chats)
		{
			foreach (var guid in announceIdsOrUser)
			{
				if (guid == ch.AnnounceId)
				{
					correctChats.Add(ch.Id);
				}
			}
		}

		var ownedChats = new List<Guid>();
		foreach (var chat in chats)
		{
			foreach (var announce in announces)
			{
				if (chat.AnnounceId == announce.Id)
				{
					ownedChats.Add(chat.Id);
				}
			}
		}

		foreach (var ownedChat in ownedChats)
		{
			await Groups.AddToGroupAsync(GetConnectionId(), ownedChat.ToString());
		}
		foreach (var chatId in chatIdsOfUser)
		{
			await Groups.AddToGroupAsync(GetConnectionId(), chatId.ToString());
		}

		foreach (var chatId in correctChats)
		{
			await Groups.AddToGroupAsync(GetConnectionId(), chatId.ToString());
		}
	}

	private Guid GetUserId()
	{
		return Guid.Parse(Context.User.GetId());
	}

	private string GetConnectionId()
	{
		return Context.ConnectionId;
	}
}