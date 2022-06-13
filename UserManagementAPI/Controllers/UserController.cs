using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.BLL.DTO;
using UserManagement.BLL.IServices;

namespace UserManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService userService;

    public UserController(IUserService userService) => this.userService = userService;

    // Get All Users Profiles
    [HttpGet("GetAll")]
    public async Task<IEnumerable<UserProfileDTO>> GetAllUsersProfiles()
    {
        return await userService.GetAllUsersProfiles();
    }

    // Get User Profile by id
    //[Authorize]
    [HttpGet("GetById")]
    public async Task<UserProfileDTO> GetById([FromQuery] string id)
    {
        return await userService.GetUserProfileById(id);
    }

    // Get User Profile by id
    //[Authorize]
    [HttpGet("GetByEmail")]
    public async Task<UserProfileDTO> GetByEmail([FromQuery] string email)
    {
        return await userService.GetUserProfileByEmail(email);
    }

    // Register new user
    [HttpPost("Register")]
    public async Task<bool> Register([FromBody] UserRegisterDTO userRegisterDTO)
    {
        return await userService.RegisterUser(userRegisterDTO);
    }

    // Delete user by id
    //[Authorize]
    [HttpDelete("Delete")]
    public async Task<bool> DeleteById([FromQuery] string id)
    {
        return await userService.DeleteUserById(id);
    }

    // Edit user
    //[Authorize]
    [HttpPost("Update")]
    public async Task<bool> EditUser([FromBody] UserEditDTO userEditDTO)
    {
        return await userService.UpdateUser(userEditDTO);
    }

    [HttpPost("Login")]
    public async Task<UserTokenDTO> Login([FromBody] UserLoginDTO login)
    {
        return await userService.LogIn(new UserLoginDTO() { Email = login.Email, Password = login.Password });
    }

    //[Authorize]
    [HttpPost("GetByToken")]
    public async Task<UserProfileDTO> GetByAccessToken([FromBody] string token)
    {
        return await userService.GetUserByAccessToken(new UserTokenDTO() { AccessToken = token });
    }
}
