using Microsoft.AspNetCore.Identity;

namespace UserManagement.DAL.Enteties
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
