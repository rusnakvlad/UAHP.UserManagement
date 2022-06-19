using UserManagement.BLL.DTO;
using UserManagement.DAL.Enteties;
using UserManagement.DAL.Models;

namespace UserManagement.BLL.IServices;

public interface IUserService
{
    public Task<PaginatedList<UserProfileDTO>> GetAllUsersProfiles(PaginationQueryModel paginationQueryModel);
    public Task<UserProfileDTO> GetUserProfileById(string id);
    public Task<UserProfileDTO> RegisterUser(UserRegisterDTO userRegisterDTO); // Add New User
    public Task<bool> DeleteUserById(string id);
    public Task<bool> UpdateUser(UserEditDTO userEditDTO);
    public Task<UserProfileDTO> GetUserProfileByEmail(string email);
    public Task<UserProfileDTO> GetUserByAccessToken(UserTokenDTO token);
    public Task<UserTokenDTO> LogIn(UserLoginDTO user);

}
