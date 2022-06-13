using AutoMapper;
using UserManagement.BLL.DTO;
using UserManagement.DAL.Enteties;

namespace UserManagement.BLL.MapperConfigurations;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<User, UserProfileDTO>();
        CreateMap<UserRegisterDTO, User>();
        CreateMap<UserEditDTO, User>();
    }
}
