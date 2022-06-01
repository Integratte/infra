using Integratte.Infra.MediatR;
using Integratte.Infra.RestSharp;

namespace Integratte.Infra.TestesDeApi
{
    public static class InjecaoDeDependencias
    {
        public static IServiceCollection IncluirDependenciasDoProjeto(this IServiceCollection services)
        {
            services.AdicionarDependenciasInfra();
            services.AdicionarDependenciasMediatR();
            services.AdicionarDependenciasRestSharp();
            return services;

        }

    }

}
