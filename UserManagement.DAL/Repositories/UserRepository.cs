using Microsoft.AspNetCore.Identity;
using UserManagement.DAL.EF;
using UserManagement.DAL.Enteties;
using UserManagement.DAL.IRepositories;

namespace UserManagement.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext context;
    public UserManager<User> UserManager { get; set; }
    public RoleManager<IdentityRole> RoleManager { get; set; }
    public SignInManager<User> SignInManager { get; set; }

    public UserRepository(ApplicationDbContext context, UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
    {
        this.context = context;
        UserManager = userManager;
        RoleManager = roleManager;
        SignInManager = signInManager;

    }

    public async Task<User> Insert(User entity)
    {
        await UserManager.CreateAsync(entity);
        await context.SaveChangesAsync();
        await UserManager.AddToRoleAsync(entity, "User");
        return await UserManager.FindByEmailAsync(entity.Email);
    }

    public async Task<bool> Delete(User entity)
    {
        var result = await UserManager.DeleteAsync(entity);
        await context.SaveChangesAsync();
        return result.Succeeded;
    }

    public async Task<bool> DeleteById(string id)
    {
        var user = await context.Users.FindAsync(id);
        return await Delete(user);
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await Task.Run(() => UserManager.Users);
    }

    public async Task<User> GetById(string id)
    {
        return await UserManager.FindByIdAsync(id);
    }

    public async Task<bool> Update(User entity)
    {
        var user = await UserManager.FindByIdAsync(entity.Id);
        user.Name = entity.Name;
        user.Surname = entity.Surname;
        user.PhoneNumber = entity.PhoneNumber;
        user.Email = entity.Email;
        user.PasswordHash = entity.PasswordHash;
        var result = await UserManager.UpdateAsync(user);
        return result.Succeeded;
    }

    public async Task<User> GetByEmail(string email)
    {
        return await UserManager.FindByEmailAsync(email);
    }

    public async Task<User> LogIn(string email, string password)
    {
        var user = await UserManager.FindByEmailAsync(email);
        if (user != null)
            return user.PasswordHash == password ? user : null;
        else return null;
    }
}
