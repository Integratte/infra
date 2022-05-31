using Integratte.Infra.Cache.EmMemoria;
using Microsoft.Extensions.DependencyInjection;

namespace Integratte.Infra.Cache;

public static class InjecaoDeDependencias
{
    public static void AdicionarDependenciasDeCache(this IServiceCollection services)
    {
        services.AddTransient<ICacheEmMemoria, CacheComMemoryCache>();

    }

}
