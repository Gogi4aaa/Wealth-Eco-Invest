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
			.Where(x => x.UserFrom == userId || x.UserTo == userId)
			.Select(x => x.Id)
			.ToListAsync();

		var announce = await dbContext.Announces
			.FirstOrDefaultAsync(x => x.UserId == userId);

		if (announce != null)
		{
			await Groups.AddToGroupAsync(GetConnectionId(), announce.Id.ToString());
		}
		var announceIdsOrUser = dbContext.Purchases
			.Where(x => x.BuyerId == userId)
			.Select(x => x.AnnounceId);

		foreach (var chatId in chatIdsOfUser)
		{
			await Groups.AddToGroupAsync(GetConnectionId(), chatId.ToString());
		}

		foreach (var announceId in announceIdsOrUser)
		{
			await Groups.AddToGroupAsync(GetConnectionId(), announceId.ToString());
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