﻿using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagement.BLL.DTO;
using UserManagement.BLL.IServices;
using UserManagement.BLL.JWT;
using UserManagement.DAL.Enteties;
using UserManagement.DAL.Extensions;
using UserManagement.DAL.Models;
using UserManagement.DAL.UnitOfWork;

namespace UserManagement.BLL.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork Database;
    private readonly IMapper mapper;
    private readonly IGrpcService grpcService;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper, IGrpcService grpcService)
    {
        Database = unitOfWork;
        this.mapper = mapper;
        this.grpcService = grpcService;
    }

    public async Task<bool> DeleteUserById(string id)
    {
        return await Database.UserRepository.DeleteById(id);
    }

    public async Task<PaginatedList<UserProfileDTO>> GetAllUsersProfiles(PaginationQueryModel paginationQueryModel)
    {
        var users = await Database.UserRepository.GetAll();

        var pagedUsers = await users.Select(u => mapper.Map<UserProfileDTO>(u))
            .PaginateListAsync(paginationQueryModel.PageNumber, paginationQueryModel.PageSize);
        return pagedUsers;
    }

    public async Task<UserProfileDTO> GetUserProfileById(string id)
    {
        var user = await Database.UserRepository.GetById(id);
        var UserDto = GetUserDTO(user);
        var userCommmentsCount = grpcService.GetUserCommentsCount(id);
        UserDto.CommentsAmount = userCommmentsCount;
        return UserDto;
    }

    public async Task<UserProfileDTO> GetUserProfileByEmail(string email)
    {
        var user = await Database.UserRepository.GetByEmail(email);
        return GetUserDTO(user);
    }

    public async Task<UserTokenDTO> LogIn(UserLoginDTO userLogin)
    {
        var user = await Database.UserRepository.LogIn(userLogin.Email, userLogin.Password);
        if (user != null)
        {
            var mappedUser = mapper.Map<User, UserProfileDTO>(user);
            return TokenManager.BuildToken(mappedUser);
        }
        return null;
    }

    public async Task<UserProfileDTO> RegisterUser(UserRegisterDTO userRegisterDTO)
    {
        var user = mapper.Map<User>(userRegisterDTO);
        var result = await Database.UserRepository.Insert(user);
        return mapper.Map<UserProfileDTO>(result);
    }

    public async Task<bool> UpdateUser(UserEditDTO userEditDTO)
    {
        return await Database.UserRepository.Update(mapper.Map<User>(userEditDTO));
    }

    private UserProfileDTO GetUserDTO(User user)
    {
        //var comments = await Database.CommentRepository.GetAll();
        //var commentsCount = comments.Where(comment => comment.UserID == user.Id).Count();
        //var ads = await Database.AdRepository.GetAll();
        //var adsCount = ads.Where(ad => ad.OwnerId == user.Id).Count();

        var mappedUser = mapper.Map<User, UserProfileDTO>(user);
        //mappedUser.AdsAmount = adsCount;
        //mappedUser.ComentsAmount = commentsCount;
        return mappedUser;
    }

    public async Task<UserProfileDTO> GetUserByAccessToken(UserTokenDTO token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(JwtOptions.KEY);

            var tokenVakidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            var principle = tokenHandler.ValidateToken(token.AccessToken, tokenVakidationParameters, out SecurityToken securityToken);

            if (securityToken is JwtSecurityToken jwtSecurityToken && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                var email = principle.FindFirst(ClaimTypes.Email)?.Value;
                var user = await Database.UserRepository.GetByEmail(email);
                return GetUserDTO(user);
            }
        }
        catch (Exception)
        {
            return new UserProfileDTO();
        }

        return new UserProfileDTO();
    }
}
