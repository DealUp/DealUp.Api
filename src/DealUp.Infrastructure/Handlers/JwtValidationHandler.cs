using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DealUp.Exceptions;
using DealUp.Infrastructure.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtConstants = DealUp.Constants.JwtConstants;

namespace DealUp.Infrastructure.Handlers;

public class JwtValidationHandler(JwtOptions jwtOptions) : JwtSecurityTokenHandler
{
    public override ClaimsPrincipal ValidateToken(string token, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
    {
        validationParameters.ValidateIssuer = true;
        validationParameters.ValidateAudience = true;
        validationParameters.ValidateIssuerSigningKey = true;
        validationParameters.ValidIssuer = JwtConstants.DefaultJwtKeyIssuerAudience;
        validationParameters.ValidAudience = JwtConstants.DefaultJwtKeyIssuerAudience;
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