using Integratte.Infra.ModuloMediador;
using Integratte.Infra.ModuloMediador.Notificacoes;
using Microsoft.AspNetCore.Mvc;
using Integratte.Infra.WebApi.Controller;
using Integratte.Infra.WebApi.JWT;
using Microsoft.AspNetCore.Authorization;

namespace Integratte.Infra.TestesDeApi.Controllers;

[ApiController]
[Route("AutenticacaoJWT")]
public class AutenticacaoJWTController : ControllerApiBase
{
    public AutenticacaoJWTController(Mediador mediador) : base(mediador) { }

    [HttpGet]
    [Route("ExemploDeToken")]
    public ActionResult<RetornoPadraoDaApi<object?>> ExemploDeToken()
    {
        var resposta = JWT.GerarTokenJWT(new() { { "ExemploDeChave", "ExemploDeValor" }, { "ExemploDeChave2", "ExemploDeValor2" } }, _mediador.Configuration);
        return EnviarResposta<object>(new { resposta.TokenJWT, resposta.Expiracao });

    }

    [Authorize]
    [HttpGet]
    [Route("ExemploDeRotaComAutorizacao")]
    public ActionResult<RetornoPadraoDaApi<object?>> ExemploDeRotaComAutorizacao()
    {
        return EnviarResposta<object>(new { conteudo = "Exemplo de conteúdo." });

    }

}
