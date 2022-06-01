using Integratte.Infra.ModuloHttp;
using Microsoft.Extensions.DependencyInjection;

namespace Integratte.Infra.RestSharp;

public static class InjecaoDeDependencias
{
    public static void AdicionarDependenciasRestSharp(this IServiceCollection services)
    {
        services.AddSingleton<ChamadaHttp, ChamadaHttpComRestSharp>();

    }


}