namespace Wealth_Eco_Invest.Hubs
{
	using Data;
	using Data.Models;
	using Microsoft.AspNetCore.SignalR;
	using Web.Infrastructure.Extensions;

	public class ChatHub : Hub
	{
		private readonly ApplicationDbContext dbContext;
		public ChatHub(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		public async Task SendMessage(string user, string message, string chatId)
		{
			await base.Clients.All.SendAsync("ReceiveMessage", user, message, chatId);
			Clients.Client(GetConnectionId());
		}

		public override Task OnConnectedAsync()
		{
			GetConnectionId();
			//dobavqm connection-Idto kum momentniq potrebitel v kolonkata
			return base.OnConnectedAsync();
		}
		public override Task OnDisconnectedAsync(Exception? exception)
		{
			//premahvam connection-Idto kum momentniq potrebitel ot kolonkata
			return base.OnDisconnectedAsync(exception);
		}

		private string GetUserId()
		{
			return Context.User.GetId();
		}
		private string GetConnectionId()
		{
			return Context.ConnectionId;
		}
	}
}