using Integratte.Infra.ModuloMediador.Comandos;
using Integratte.Infra.ModuloMediador.Consultas;
using Integratte.Infra.ModuloMediador.Eventos;
using Integratte.Infra.ModuloMediador.Notificacoes;
using Microsoft.Extensions.Configuration;

namespace Integratte.Infra.ModuloMediador;

public abstract class Mediador
{
    protected readonly NotificacoesDoMediador _notificacoes;
    protected readonly IEventosDoMediador _eventos;
    protected readonly IComandosDoMediador _comandos;
    protected readonly IConsultasDoMediador _consultas;

    public IConfiguration Configuration { get; }

    protected Mediador(IConfiguration configuration, NotificacoesDoMediador notificacoes, IEventosDoMediador eventos, IComandosDoMediador comandos, IConsultasDoMediador consultas)
    {
        Configuration = configuration;

        _notificacoes = notificacoes;
        _eventos = eventos;
        _comandos = comandos;
        _consultas = consultas;

    }

    #region Notificações

    public bool SemImpedimentos => _notificacoes.SemImpedimentos;
    public bool ComImpedimentos => !_notificacoes.SemImpedimentos;
    public INotificacao[] Notificacoes => _notificacoes.Listar;
    public void AdicionarNotificacao(string mensagem, bool exibirParaUsuario = true, TipoDeNotificacaoEnum tipo = TipoDeNotificacaoEnum.RequisicaoInvalida)
    {
        _notificacoes.Adicionar(mensagem, exibirParaUsuario, tipo);

    }

    #endregion

    public async Task PublicarEvento(IEvento evento)
    {
        if (_notificacoes.FoiInterrompido)
            return;

        await _eventos.Publicar(evento);

    }

    public async Task ExecutarComando(IComando comando)
    {
        if (_notificacoes.FoiInterrompido)
            return;

        await _comandos.Executar(comando);

    }
    public async Task<RetornoT?> ExecutarComando<RetornoT>(IComando<RetornoT> comando) where RetornoT : IRetornoDoComando
    {
        if (_notificacoes.FoiInterrompido)
            return default;

        return await _comandos.Executar(comando);

    }

    public async Task<RetornoT?> Consultar<RetornoT>(IConsulta<RetornoT> consulta) where RetornoT : IRetornoDaConsulta
    {
        if (_notificacoes.FoiInterrompido)
            return default;

        return await _consultas.Consultar(consulta);

    }

}
