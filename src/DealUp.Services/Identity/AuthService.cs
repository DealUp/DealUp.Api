using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DealUp.Constants;
using DealUp.Domain.Auth;
using DealUp.Domain.Identity.Interfaces;
using DealUp.Domain.User.Interfaces;
using DealUp.Exceptions;
using DealUp.Infrastructure.Configuration.Options;
using DealUp.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UserDomain = DealUp.Domain.User.User;

namespace DealUp.Services.Identity;

public class AuthService(IOptions<JwtOptions> jwtOptions, IUserRepository userRepository) : IAuthService
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public async Task<JwtToken> RegisterUserAsync(Credentials credentials)
    {
        var existingUser = await userRepository.GetUserByUsernameAsync(credentials.Username);
        if (existingUser is not null)
        {
            throw new InvalidUserException($"User with email: {existingUser.Username} already exists.");
        }

        var newUser = UserDomain.CreateNew(credentials.Username, credentials.Password);
        await userRepository.SaveUserAsync(newUser);

        return BuildJwtToken(newUser);
    }

    public async Task<JwtToken> GetTokenAsync(Credentials credentials)
    {
        var user = await userRepository.GetUserByUsernameAsync(credentials.Username);
        if (user?.IsMatchingPassword(credentials) is not true)
        {
            throw new InvalidUserException("User does not exist or password is wrong.");
        }

        return BuildJwtToken(user);
    }

    private JwtToken BuildJwtToken(UserDomain user)
    {
        var expirationTimeSpan = TimeSpan.FromMinutes(_jwtOptions.MinutesToExpire);

        ClaimsIdentity claimsIdentity = BuildClaims(user);
        DateTime now = DateTime.UtcNow;
        DateTime expires = now.Add(expirationTimeSpan);
        var securityKey = new SymmetricSecurityKey(_jwtOptions.Secret.ToBytes());

        var jwtSecurityToken = new JwtSecurityToken(
            _jwtOptions.Issuer,   
            _jwtOptions.Audience,
            claimsIdentity.Claims,
            now,
            expires,
            new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256));

        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        return new JwtToken(JwtBearerDefaults.AuthenticationScheme, accessToken, expirationTimeSpan.Seconds);
    }

    private static ClaimsIdentity BuildClaims(UserDomain user)
    {
        return new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimsConstants.UserStatus, user.Status.ToString())
            ],
            JwtBearerDefaults.AuthenticationScheme,
            ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);
    }
}