using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DealUp.Domain.Auth;
using DealUp.Domain.Auth.Interfaces;
using DealUp.Domain.User;
using DealUp.Exceptions;
using DealUp.Infrastructure.Configuration.Options;
using DealUp.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DealUp.Services;

public class AuthService(IOptions<JwtOptions> jwtOptions, IUserRepository userRepository) : IAuthService
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public async Task<JwtToken> GetTokenAsync(Credentials credentials)
    {
        var user = await userRepository.GetUserAsync(credentials.Username);
        if (user.Sha256Password == credentials.Password)
        {
            return BuildJwtToken(user);
        }

        throw new InvalidUserException("User doesn't exist or password is wrong.");
    }
    
    private JwtToken BuildJwtToken(User user)
    {
        ClaimsIdentity claimsIdentity = BuildClaims(user);
        DateTime now = DateTime.UtcNow;
        DateTime expires = now.Add(TimeSpan.FromMinutes(_jwtOptions.MinutesToExpire));

        var jwtSecurityToken = new JwtSecurityToken(
            _jwtOptions.Issuer,   
            _jwtOptions.Audience,
            notBefore: now,
            claims: claimsIdentity.Claims,
            expires: expires,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(_jwtOptions.Secret.ToBytes()), 
                SecurityAlgorithms.HmacSha256));

        return new JwtToken(new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken));
    }
    
    private static ClaimsIdentity BuildClaims(User user)
    {
        return new ClaimsIdentity(
            new List<Claim> {
                new("Id", user.Id.ToString()),
                new("Username", user.Username)
            },
            JwtBearerDefaults.AuthenticationScheme,
            ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);
    }
}