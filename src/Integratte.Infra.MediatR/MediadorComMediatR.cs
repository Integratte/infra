using Integratte.Infra.ModuloMediador;
using Integratte.Infra.ModuloMediador.Comandos;
using Integratte.Infra.ModuloMediador.Eventos;
using Integratte.Infra.ModuloMediador.Notificacoes;

namespace Integratte.Infra.MediatR
{
    internal class MediadorComMediatR : Mediador
    {
        public MediadorComMediatR(NotificacoesDoMediador notificacoes, IEventosDoMediador eventos, IComandosDoMediador comandos) : base(notificacoes, eventos, comandos)
        {
        }

    }

}
