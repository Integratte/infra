using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Integratte.Infra.JWT;

public static class TokenJwt
{
    public static (DateTimeOffset Expiracao, string TokenJWT) Gerar(Dictionary<string, string> claims, IConfiguration configuration)
    {
        var expiracaoEmMinutos = int.Parse(configuration["JWT:ExpiracaoDoTokenEmMinutos"]);
        var chaveSecreta = configuration["JWT:ChaveSecreta"];
        return Gerar(claims, expiracaoEmMinutos, chaveSecreta);

    }

    public static (DateTimeOffset Expiracao, string TokenJWT) Gerar(Dictionary<string, string> claims, int expiracaoEmMinutos, string chaveSecreta)
    {
        DateTimeOffset expiracao = DefinirExpiracao();
        byte[] bytesDaChaveSecreta = DefinirChaveSecreta();
        var tokenJWT = GerarToken(claims);

        return (expiracao, tokenJWT);

        #region SubMétodos

        DateTimeOffset DefinirExpiracao()
        {
            return DateTime.UtcNow.AddMinutes(expiracaoEmMinutos);

        }

        byte[] DefinirChaveSecreta()
        {
            return Encoding.ASCII.GetBytes(chaveSecreta);

        }

        string GerarToken(Dictionary<string, string> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.Select(x => new Claim(x.Key, x.Value))),
                Expires = expiracao.DateTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(bytesDaChaveSecreta), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }

        #endregion

    }

}
