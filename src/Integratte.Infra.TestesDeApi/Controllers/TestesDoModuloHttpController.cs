using Integratte.Infra.ModuloMediador;
using Integratte.Infra.TestesDeApi.ModuloHttp;
using Integratte.Infra.WebApi.Controller;
using Microsoft.AspNetCore.Mvc;

namespace Integratte.Infra.TestesDeApi.Controllers;

[ApiController]
[Route("TestesDoModuloHttpController")]
public class TestesDoModuloHttpController : ControllerApiBase
{
    public TestesDoModuloHttpController(Mediador mediador) : base(mediador) { }

    [HttpGet]
    [Route("ObterTokenAtual")]
    public async Task<ActionResult<RetornoPadraoDaApi<RetornoJwt?>>> ObterTokenAtual()
    {
        var (tokenDeAcesso, expiracao) = await TokenParaApiDeTestes.ObterInstancia().ObterTokenAtual();
        return EnviarResposta(new RetornoJwt(tokenDeAcesso, expiracao));

    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class RetornoJwt
    {
        public RetornoJwt() { }

        public RetornoJwt(string token, DateTimeOffset expiracao)
        {
            Token = token;
            Expiracao = expiracao;
        }

        public string Token { get; set; }
        public DateTimeOffset Expiracao { get; set; }

    }

}
