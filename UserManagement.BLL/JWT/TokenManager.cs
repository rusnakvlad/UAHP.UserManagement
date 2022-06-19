using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagement.BLL.DTO;

namespace UserManagement.BLL.JWT;

public static class TokenManager
{
    public static UserTokenDTO BuildToken(UserProfileDTO userProfile)
    {
        var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, userProfile.Id),
                new Claim(ClaimTypes.Name, userProfile.Name),
                new Claim(ClaimTypes.Surname, userProfile.Surname),
                new Claim(ClaimTypes.Email, userProfile.Email),
                new Claim(ClaimTypes.MobilePhone, userProfile.PhoneNumber)
            };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.KEY));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiration = DateTime.UtcNow.AddDays(JwtOptions.LIFETIME);

        JwtSecurityToken token = new(
            issuer: JwtOptions.ISSUER,
            audience: JwtOptions.AUDIENCE,
            claims: claims,
            expires: expiration,
            signingCredentials: creds);

        return new UserTokenDTO() { AccessToken = new JwtSecurityTokenHandler().WriteToken(token) };
    }
}
