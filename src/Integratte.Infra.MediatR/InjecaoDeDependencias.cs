using Integratte.Infra.MediatR.Comandos;
using Integratte.Infra.MediatR.Consultas;
using Integratte.Infra.MediatR.Eventos;
using Integratte.Infra.MediatR.Notificacoes;
using Integratte.Infra.ModuloMediador;
using Integratte.Infra.ModuloMediador.Comandos;
using Integratte.Infra.ModuloMediador.Consultas;
using Integratte.Infra.ModuloMediador.Eventos;
using Integratte.Infra.ModuloMediador.Notificacoes;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Integratte.Infra.MediatR
{
    public static class InjecaoDeDependencias
    {
        public static void AdicionarDependenciasMediatR(this IServiceCollection services)
        {
            CarregarAssembliesMediatR(services, typeof(MediadorComMediatR).Assembly);
            services.AddScoped<Mediador, MediadorComMediatR>();
            services.AddScoped<NotificacoesDoMediador, NotificacoesDoMediadorImp>();
            services.AddScoped<IEventosDoMediador, EventosDoMediador>();
            services.AddScoped<IComandosDoMediador, ComandosDoMediador>();
            services.AddScoped<IConsultasDoMediador, ConsultasDoMediador>();

        }

        public static IServiceCollection CarregarAssembliesMediatR(this IServiceCollection services, Assembly assembly)
        {
            services.AddMediatR(assembly);
            return services;

        }

    }

}
