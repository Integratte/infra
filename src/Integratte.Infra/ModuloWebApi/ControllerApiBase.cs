using Integratte.Infra.ModuloMediador;
using Integratte.Infra.ModuloMediador.Notificacoes;
using Microsoft.AspNetCore.Mvc;

namespace Integratte.Infra.ModuloWebApi;

public class ControllerApiBase : ControllerBase
{
    protected readonly Mediador _mediador;

    public ControllerApiBase(Mediador mediador)
    {
        _mediador = mediador;

    }

    protected ActionResult<RetornoPadraoDaApi<T?>> EnviarResposta<T>(T? resposta) where T : new()
    {
        var retorno = RetornoPadrao(respostaNula: resposta == null);

        if (retorno.CodigoDoStatus == 200)
            return StatusCode(200, new RetornoPadraoDaApi<T?>(resposta, _mediador.Notificacoes));

        return StatusCode(retorno.CodigoDoStatus, retorno.RetornoPadraoDaApi);

    }

    protected ActionResult<RetornoPadraoDaApi> SemResposta()
    {
        var retorno = RetornoPadrao(false);
        return StatusCode(retorno.CodigoDoStatus, retorno.RetornoPadraoDaApi);

    }

    private (int CodigoDoStatus, RetornoPadraoDaApi RetornoPadraoDaApi) RetornoPadrao(bool respostaNula)
    {
        var codigoDoStatus = DefinirCodigoDeStatus(respostaNula);

        switch (codigoDoStatus)
        {
            case 500:
            case 400:
                return (codigoDoStatus, new RetornoPadraoDaApi(_mediador.Notificacoes, sucedido: false));

            case 404:
                {
                    _mediador.AdicionarNotificacao("Recurso nulo ou não encontrado", exibirParaUsuario: false);
                    return (codigoDoStatus, new RetornoPadraoDaApi(_mediador.Notificacoes, sucedido: false));

                }

            default:
                return (codigoDoStatus, new RetornoPadraoDaApi(_mediador.Notificacoes, sucedido: true));

        }

    }

    private int DefinirCodigoDeStatus(bool respostaNula = false)
    {
        if (_mediador.Notificacoes.Any(x => x.TipoDeNotificacaoEnum == TipoDeNotificacaoEnum.ErroDoSistema))
            return 500; // Erro Interno no Servidor

        if (_mediador.Notificacoes.Any(x => x.TipoDeNotificacaoEnum == TipoDeNotificacaoEnum.RequisicaoInvalida))
            return 400; // Requisição Inválida

        if (respostaNula)
            return 404; //Recurso não Encontrado

        return 200;

    }

}

