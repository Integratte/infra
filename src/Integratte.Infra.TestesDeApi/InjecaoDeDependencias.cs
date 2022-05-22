using Integratte.Infra.MediatR;

namespace Integratte.Infra.TestesDeApi
{
    public static class InjecaoDeDependencias
    {
        public static IServiceCollection IncluirDependenciasDoProjeto(this IServiceCollection services)
        {
            services.AdicionarDependenciasMediatR();
            return services;

        }

    }

}
