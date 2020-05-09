using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobSolution.Repository.Concrete
{
    public class ChatRepository : Hub
    {
        public Task SendMessage(string jwt, string message)
        {
            Console.WriteLine(Context.UserIdentifier + " len=" + Context.Items.Count.ToString() + "  == " + Context.UserIdentifier);
            return Clients.All.SendAsync("ReceiveMessage", jwt, message);
        }
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("-------------------------conn");
            

            await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("-------------------------desc");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
