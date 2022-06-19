using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.BLL.DTO;
using UserManagement.BLL.IServices;
using UserManagement.DAL.Models;

namespace UserManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService userService;
    private readonly IBus bus;
    public UserController(IUserService userService, IBus bus)
    {
        this.userService = userService;
        this.bus = bus;
    }
    // Get All Users Profiles
    [HttpGet("GetAll")]
    public async Task<PaginatedList<UserProfileDTO>> GetAllUsersProfiles([FromQuery] PaginationQueryModel paginationQueryModel)
    {
        return await userService.GetAllUsersProfiles(paginationQueryModel);
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
    public async Task<UserProfileDTO> Register([FromBody] UserRegisterDTO userRegisterDTO)
    {
        var result = await userService.RegisterUser(userRegisterDTO);

        var user = new UAHause.CommonWork.Models.User
        {
            Id = result.Id,
            Email = result.Email,
            Name = result.Name,
            Surname = result.Surname,
            Phone = result.PhoneNumber
        };

        bus.Publish(user);

        return result;
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
