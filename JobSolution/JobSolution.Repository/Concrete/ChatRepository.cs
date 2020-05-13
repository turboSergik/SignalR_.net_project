using JobSolution.Infrastructure.Database;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSolution.Repository.Concrete
{
    public class ChatRepository : Hub
    {

        private IServiceProvider _sp;
        private static Dictionary<int, bool> IsEmployerOnlineInGroup = new Dictionary<int, bool>();

        public ChatRepository(IServiceProvider sp)
        {
            _sp = sp;
        }

        public Task SendMessage(string jwt, int employerId, string message)
        {

            JwtSecurityToken token = new JwtSecurityToken(jwt);
            var claims = token.Claims;

            string email = "";

            /// Parse user Data fron JWT
            foreach (var item in claims)
            {
                if (item.Type == "email") email = item.Value;
            }

            return Clients.Group(employerId.ToString()).SendAsync("ReceiveMessage", email, IsUserEmployer(employerId, jwt), message);
        }

        public Task JoinRoom(int employerId, string jwt)
        {
            Groups.AddToGroupAsync(Context.ConnectionId, employerId.ToString());
            if (IsUserEmployer(employerId, jwt))
            {
                Clients.Group(employerId.ToString()).SendAsync("OnEmployerOnline");
                if (IsEmployerOnlineInGroup.ContainsKey(employerId)) IsEmployerOnlineInGroup[employerId] = true;
                else IsEmployerOnlineInGroup.Add(employerId, true);
            }
            else if (IsEmployerOnlineInGroup.ContainsKey(employerId) && IsEmployerOnlineInGroup[employerId] == true) Clients.Group(employerId.ToString()).SendAsync("OnEmployerOnline");

            return Task.FromResult<object>(null);
        }

        public Task LeaveRoom(int employerId, string jwt)
        {
            Console.WriteLine("===================leave" + IsUserEmployer(employerId, jwt).ToString());
            if (IsUserEmployer(employerId, jwt))
            {
                Clients.Group(employerId.ToString()).SendAsync("OnEmployerOffline");
                IsEmployerOnlineInGroup[employerId] = false;
            }
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, employerId.ToString());
        }


        public bool IsUserEmployer(int employerId, string jwt)
        {

            JwtSecurityToken token = new JwtSecurityToken(jwt);
            var claims = token.Claims;
            int user_id = 0;

            /// Parse user Data fron JWT
            foreach (var item in claims)
            {
                if (item.Type == "UserId") user_id = Int32.Parse(item.Value);
            }

            bool isEmployer = false;

            // Using Db
            using (var scope = _sp.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var current_job = dbContext.Jobs.FirstOrDefault(item => item.Id == employerId);

                if (current_job.UserId == user_id) isEmployer = true;
                else isEmployer = false;
            }

            return isEmployer;
        }
    }
}
