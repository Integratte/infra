using Integratte.Infra.ModuloMediador;
using Integratte.Infra.ModuloMediador.Comandos;
using Integratte.Infra.ModuloMediador.Consultas;
using Integratte.Infra.ModuloMediador.Eventos;
using Integratte.Infra.ModuloMediador.Notificacoes;
using Microsoft.Extensions.Configuration;

namespace Integratte.Infra.MediatR
{
    internal class MediadorComMediatR : Mediador
    {
        public MediadorComMediatR(IConfiguration configuration, NotificacoesDoMediador notificacoes, IEventosDoMediador eventos, IComandosDoMediador comandos, IConsultasDoMediador consultas) : base(configuration, notificacoes, eventos, comandos, consultas)
        {
        }

    }

}
