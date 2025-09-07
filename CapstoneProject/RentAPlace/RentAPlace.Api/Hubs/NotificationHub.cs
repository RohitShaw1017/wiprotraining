// Hubs/NotificationHub.cs
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;


namespace RentAPlace.Api.Hubs
{
    public class NotificationHub : Hub
    {

        // we will use "user" identifier as the numeric user id from claims
        public override Task OnConnectedAsync()
        {
            // Optionally add the connection to a user group by claim
            // But we'll have client call a "Subscribe" method passing userId
            return base.OnConnectedAsync();
        }

        public Task Subscribe(string userId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, $"user_{userId}");
        }

        public Task Unsubscribe(string userId)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, $"user_{userId}");
        }
    }
}
