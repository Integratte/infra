using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Integratte.Infra.WebApi.JWT;

public static class JWT
{
    public static void ImplementarAutenticacao(this IServiceCollection services, IConfiguration configuration)
    {
        var key = Encoding.ASCII.GetBytes(configuration["JWT:ChaveSecreta"]);
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = true;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
            };
        });

    }

    public static (DateTimeOffset Expiracao, string TokenJWT) GerarTokenJWT(Dictionary<string, string> claims, IConfiguration configuration)
    {
        DateTimeOffset expiracao = DefinirExpiracao();
        byte[] chaveSecreta = DefinirChaveSecreta();
        var tokenJWT = GerarToken(claims);

        return (expiracao, tokenJWT);

        #region SubMétodos

        DateTimeOffset DefinirExpiracao()
        {
            var expiracaoEmMinutos = double.Parse(configuration["JWT:ExpiracaoDoTokenEmMinutos"]);
            return DateTime.UtcNow.AddMinutes(expiracaoEmMinutos);

        }

        byte[] DefinirChaveSecreta()
        {
            var chave = configuration["JWT:ChaveSecreta"];
            return Encoding.ASCII.GetBytes(chave);

        }

        string GerarToken(Dictionary<string, string> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.Select(x => new Claim(x.Key, x.Value))),
                Expires = expiracao.DateTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chaveSecreta), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }

        #endregion

    }

}
