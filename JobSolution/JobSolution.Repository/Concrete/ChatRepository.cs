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
        private Dictionary<int, bool> IsEmployerOnlineInGroup;

        public ChatRepository(IServiceProvider sp)
        {
            _sp = sp;
            IsEmployerOnlineInGroup = new Dictionary<int, bool>();
        }

        public Task SendMessage(string jwt, int jobId, string message)
        {

            JwtSecurityToken token = new JwtSecurityToken(jwt);
            var claims = token.Claims;

            string email = "";

            // Parse user Data fron JWT
            foreach (var item in claims)
            {
                if (item.Type == "email") email = item.Value;
            }

            // Send message to all users in group
            return Clients.Group(jobId.ToString()).SendAsync("ReceiveMessage", email, IsUserEmployer(jobId, jwt), message);
        }

        public Task JoinRoom(int jobId, string jwt)
        {
            // Check, if joined user is employer
            if (IsUserEmployer(jobId, jwt))
            {
                // Set that Employer online
                Clients.Group(jobId.ToString()).SendAsync("OnEmployerOnline");

                // Set employer 
                IsEmployerOnlineInGroup.Add(jobId, true);
            }
            else if (IsEmployerOnlineInGroup.ContainsKey(jobId) && IsEmployerOnlineInGroup[jobId] == true) Clients.Group(jobId.ToString()).SendAsync("OnEmployerOnline");

            // Add user to group
            return Groups.AddToGroupAsync(Context.ConnectionId, jobId.ToString());
        }

        public Task LeaveRoom(int jobId, string jwt)
        {
            if (IsUserEmployer(jobId, jwt))
            {
                Clients.Group(jobId.ToString()).SendAsync("OnEmployerOffline");
                IsEmployerOnlineInGroup[jobId] = false;
            }
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, jobId.ToString());
        }


        public bool IsUserEmployer(int jobId, string jwt)
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
                var current_job = dbContext.Jobs.FirstOrDefault(item => item.Id == jobId);

                if (current_job.UserId == user_id) isEmployer = true;
                else isEmployer = false;
            }

            return isEmployer;
        }
    }
}
