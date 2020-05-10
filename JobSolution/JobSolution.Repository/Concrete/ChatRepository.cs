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
        public ChatRepository(IServiceProvider sp)
        {
            _sp = sp;
        }

        public Task SendMessage(string jwt, int employerId, string message)
        {

            JwtSecurityToken token = new JwtSecurityToken(jwt);
            var claims = token.Claims;

            string email = "";
            int user_id = 0;

            /// Parse user Data
            foreach (var item in claims)
            {
                // Console.WriteLine(item.Type + ": " + item.Value);
                if (item.Type == "email") email = item.Value;
                if (item.Type == "UserId") user_id = Int32.Parse(item.Value);

            }

            // Console.WriteLine("empl id=" + employerId.ToString() + " user_id=" + user_id);

            bool isEmployer = false;

            using (var scope = _sp.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var current_job = dbContext.Jobs.FirstOrDefault(item => item.Id == employerId);

                if (current_job.UserId == user_id) isEmployer = true;
                else isEmployer = false;

                // Console.WriteLine("COUNT OF JOBS====" + dbContext.Jobs.ToList().Count.ToString());
            }

            // Console.WriteLine("================================================");
            // Console.WriteLine("Email: " + email + " is empl: " + isEmployer + " mess=" + message);

            return Clients.Group(employerId.ToString()).SendAsync("ReceiveMessage", email, isEmployer, message);
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
