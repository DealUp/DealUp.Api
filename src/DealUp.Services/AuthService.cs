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

    public async Task<JwtToken> RegisterUserAsync(Credentials credentials)
    {
        var existingUser = await userRepository.GetUserAsync(credentials.Username);
        if (existingUser is not null)
        {
            throw new InvalidUserException($"User with email: {existingUser.Email} already exists.");
        }

        var newUser = new User(credentials.Username, credentials.Password);
        await userRepository.SaveUserAsync(newUser);

        return BuildJwtToken(newUser);
    }

    public async Task<JwtToken> GetTokenAsync(Credentials credentials)
    {
        var user = await userRepository.GetUserAsync(credentials.Username);
        if (user?.IsMatchingPassword(credentials) is true)
        {
            return BuildJwtToken(user);
        }

        throw new InvalidUserException("User does not exist or password is wrong.");
    }

    private JwtToken BuildJwtToken(User user)
    {
        ClaimsIdentity claimsIdentity = BuildClaims(user);
        DateTime now = DateTime.UtcNow;
        DateTime expires = now.Add(TimeSpan.FromMinutes(_jwtOptions.MinutesToExpire));
        var securityKey = new SymmetricSecurityKey(_jwtOptions.Secret.ToBytes());

        var jwtSecurityToken = new JwtSecurityToken(
            _jwtOptions.Issuer,   
            _jwtOptions.Audience,
            claimsIdentity.Claims,
            now,
            expires,
            new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256));

        return new JwtToken(new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken));
    }

    private static ClaimsIdentity BuildClaims(User user)
    {
        return new ClaimsIdentity(
            [
                new Claim("Id", user.Id.ToString()),
                new Claim("Email", user.Email)
            ],
            JwtBearerDefaults.AuthenticationScheme,
            ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);
    }
}