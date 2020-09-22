using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace NotificationHub
{
    public class NewsHub : Hub
    {
        public async Task Send(string message)
        {
            // Call the broadcastMessage method to update clients.
            await Clients.All.SendAsync("broadcastMessage", message);
        }
    }
}

