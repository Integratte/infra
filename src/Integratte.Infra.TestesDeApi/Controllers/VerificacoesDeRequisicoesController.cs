using Integratte.Infra.ModuloMediador;
using Integratte.Infra.ModuloMediador.Notificacoes;
using Microsoft.AspNetCore.Mvc;
using Integratte.Infra.WebApi.Controller;

namespace Integratte.Infra.TestesDeApi.Controllers;

[ApiController]
[Route("Verificacoes/Requisicoes")]
public class VerificacoesDeRequisicoesController : ControllerApiBase
{
    public VerificacoesDeRequisicoesController(Mediador mediador) : base(mediador) { }

    [HttpGet]
    [Route("Status200")]
    public ActionResult<RetornoPadraoDaApi> Status200()
    {
        return SemResposta();

    }

    [HttpGet]
    [Route("Status200ComNotificacao")]
    public ActionResult<RetornoPadraoDaApi> Status200ComNotificacao()
    {
        _mediador.AdicionarNotificacao("Informação com status 200 :)", exibirParaUsuario: true, TipoDeNotificacaoEnum.Informacao);
        _mediador.AdicionarNotificacao("Ponto de atenção com status 200 :)", exibirParaUsuario: true, TipoDeNotificacaoEnum.PontoDeAtencao);
        return SemResposta();

    }

    [HttpGet]
    [Route("Status200ComResposta")]
    public ActionResult<RetornoPadraoDaApi<RespostaExemplo?>> Status200ComResposta()
    {
        _mediador.AdicionarNotificacao("Opa, retornando com resposta e 200 :)", exibirParaUsuario: false, TipoDeNotificacaoEnum.Informacao);
        return EnviarResposta(new RespostaExemplo());

    }
    public class RespostaExemplo
    {
        public string Conteudo { get; set; } = "Este é um conteudo de exemplo.";

    }

    [HttpGet]
    [Route("Status500")]
    public ActionResult<RetornoPadraoDaApi> Status500()
    {
        _mediador.AdicionarNotificacao("Erro do Sistema com status 500 :)", exibirParaUsuario: true, TipoDeNotificacaoEnum.ErroDoSistema);
        return SemResposta();

    }

    [HttpGet]
    [Route("Status500DeErroGlobal")]
    public ActionResult<RetornoPadraoDaApi> Status500DeErroGlobal()
    {
        throw new Exception("Este é um erro global lançado propositalmente.");

    }

    [HttpGet]
    [Route("Status400")]
    public ActionResult<RetornoPadraoDaApi> Status400()
    {
        _mediador.AdicionarNotificacao("Requisição Inválida com status 400 :)", exibirParaUsuario: true, TipoDeNotificacaoEnum.RequisicaoInvalida);
        return SemResposta();

    }

    [HttpGet]
    [Route("Status404")]
    public ActionResult<RetornoPadraoDaApi<RespostaExemplo?>> Status404()
    {
        return EnviarResposta<RespostaExemplo?>(null);

    }

}
