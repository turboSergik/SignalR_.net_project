using Microsoft.AspNetCore.Identity;

namespace JobSolution.Domain.Auth
{
    public class Role: IdentityRole<int>
    {
        public Role() : base()
        {

        }
        public Role(string roleName) : this()
        {
            Name = roleName;
        }
    }
}
