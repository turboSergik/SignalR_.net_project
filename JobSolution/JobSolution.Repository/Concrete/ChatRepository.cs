using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobSolution.Repository.Concrete
{
    public class ChatRepository : Hub
    {
        public Task SendMessage(string jwt, int employerId, string message)
        {
            Console.WriteLine(Context.ConnectionId);
            return Clients.Group(employerId.ToString()).SendAsync("ReceiveMessage", jwt, message);
        }

        public Task JoinRoom(int employerId)
        {
            Console.WriteLine("-------------------------conn" + employerId.ToString());
            return Groups.AddToGroupAsync(Context.ConnectionId, employerId.ToString());
        }

        public Task LeaveRoom(int employerId)
        {
            Console.WriteLine("-------------------------desc" + employerId.ToString());
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, employerId.ToString());
        }
    }
}
