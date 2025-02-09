using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DealUp.Exceptions;
using DealUp.Infrastructure.Configuration.Options;
using Microsoft.IdentityModel.Tokens;
using JwtConstants = DealUp.Constants.JwtConstants;

namespace DealUp.Infrastructure.Configuration.Middlewares;

public class JwtValidationHandler(JwtOptions jwtOptions) : JwtSecurityTokenHandler
{
    public override ClaimsPrincipal ValidateToken(string token, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
    {
        validationParameters.ValidateIssuer = true;
        validationParameters.ValidateAudience = true;
        validationParameters.ValidateIssuerSigningKey = true;
        validationParameters.ValidIssuer = JwtConstants.DEFAULT_JWT_KEY_ISSUER_AUDIENCE;
        validationParameters.ValidAudience = JwtConstants.DEFAULT_JWT_KEY_ISSUER_AUDIENCE;
        validationParameters.ValidateLifetime = true;
        validationParameters.ClockSkew = TimeSpan.Zero;
        validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.Secret));

        try
        {
            return base.ValidateToken(token, validationParameters, out validatedToken);
        }
        catch (Exception ex)
        {
            throw new TokenInvalidErrorException(ex);
        }
    }
}