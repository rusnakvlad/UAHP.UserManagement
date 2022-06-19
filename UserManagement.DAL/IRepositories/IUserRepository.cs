using Microsoft.AspNetCore.Identity;
using UserManagement.DAL.Enteties;
using UserManagement.DAL.Models;

namespace UserManagement.DAL.IRepositories;

public interface IUserRepository
{
    RoleManager<IdentityRole> RoleManager { get; set; }
    SignInManager<User> SignInManager { get; set; }
    UserManager<User> UserManager { get; set; }

    Task<bool> Delete(User entity);
    Task<bool> DeleteById(string id);
    Task<IQueryable<User>> GetAll();
    Task<User> GetByEmail(string email);
    Task<User> GetById(string id);
    Task<User> Insert(User entity);
    Task<User> LogIn(string email, string password);
    Task<bool> Update(User entity);
}
