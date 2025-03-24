using DealUp.Domain.Identity;
using DealUp.Domain.Identity.Interfaces;
using DealUp.Domain.User.Interfaces;
using DealUp.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using UserDomain = DealUp.Domain.User.User;

namespace DealUp.Services.Identity.SsoProviders;

public class GoogleSsoService(
    IHttpContextService httpContextService,
    IAuthService authService,
    IUserRepository userRepository,
    IOptions<OauthOptions> options)
    : ISsoProviderService
{
    public string AuthenticationScheme => GoogleDefaults.AuthenticationScheme;

    public SsoAuthenticationProperties GetAuthenticationProperties()
    {
        var baseLoginUrl = options.Value.GoogleProviderOptions.LoginUrl;
        var loginUrl = QueryHelpers.AddQueryString(baseLoginUrl, "name", GoogleDefaults.DisplayName);
        return SsoAuthenticationProperties.Create(loginUrl, AuthenticationScheme);
    }

    public async Task<JwtToken> LoginViaSsoProviderAsync()
    {
        var ssoCredentials = await httpContextService.AuthenticateAsync(AuthenticationScheme);
        var username = ssoCredentials.Username ?? ssoCredentials.Id;

        var user = await userRepository.GetUserByUsernameAsync(username);
        if (user is null)
        {
            user = UserDomain.CreateNew(username, null);
            await userRepository.SaveUserAsync(user);
        }

        await httpContextService.SignOutAsync(AuthenticationScheme);
        return authService.BuildJwt(user);
    }
}