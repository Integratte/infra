using Integratte.Infra.ModuloConfiguracoes;
using Integratte.Infra.ModuloEmails;
using Microsoft.Extensions.DependencyInjection;

namespace Integratte.Infra
{
    public static class InjecaoDeDependencias
    {
        public static void AdicionarDependenciasInfra(this IServiceCollection services)
        {
            services.AddTransient<EnvioDeEmail, EnvioDeEmailComSystemNet>();
            services.AddTransient<IConfiguracoes, Configuracoes>();

        }

    }

}
