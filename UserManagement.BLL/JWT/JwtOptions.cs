using Microsoft.IdentityModel.Tokens;

namespace UserManagement.BLL.JWT;

public static class JwtOptions
{
    public const string ISSUER = "https://localhost:44365";
    public const string AUDIENCE = "https://localhost:44328";
    public const string KEY = "ThEHouseSeCRetKeyOfJwTtoUseItONReQuEStwiTHaUtHoriZE";
    public const int LIFETIME = 365;

    public static bool ValidateLifeTime(DateTime? notBefore, DateTime? expires, SecurityToken tokenToValidate, TokenValidationParameters @param)
    {
        return (expires != null && expires > DateTime.UtcNow);
    }
}
