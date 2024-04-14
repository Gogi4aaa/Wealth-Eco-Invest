namespace Wealth_Eco_Invest.Hubs
{
	using Data;
	using Data.Models;
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
		public async Task SendMessage(string user, string message, string chatId, string userTypingClasses)
		{
			await base.Clients.Group(chatId).SendAsync("ReceiveMessage", user, message, chatId, userTypingClasses);
		}

		public override async Task OnConnectedAsync()
		{
			var user = await this.dbContext.Users.FindAsync(GetUserId());
			user.ConnectionId = GetConnectionId();
			await this.dbContext.SaveChangesAsync();
			await AddUserToGroups();
			//dobavqm connection-Idto kum momentniq potrebitel v kolonkata
			await base.OnConnectedAsync();
		}
		public override async Task OnDisconnectedAsync(Exception? exception)
		{
			var user = await this.dbContext.Users.FindAsync(GetUserId());
			user.ConnectionId = null;
			await this.dbContext.SaveChangesAsync();
			//premahvam connection-Idto kum momentniq potrebitel ot kolonkata
			await base.OnDisconnectedAsync(exception);
		}

		private async Task AddUserToGroups()
		{
			Guid userId = GetUserId();
			var chatIdsOfUser = dbContext.Chats
				.Where(x => x.UserFrom == userId || x.UserTo == userId)//moje i da grumne zaradi userTo
				.Select(x => x.Id)
				.ToList();

			foreach (Guid chatId in chatIdsOfUser)
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
}