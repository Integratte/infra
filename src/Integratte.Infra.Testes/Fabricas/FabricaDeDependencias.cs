using Integratte.Infra.MediatR;
using Integratte.Infra.ModuloMediador;
using Microsoft.Extensions.DependencyInjection;

namespace Integratte.Infra.Testes.Fabricas;

internal class FabricaDeDependencias
{
    public Mediador Mediador { get; private set; }

    private FabricaDeDependencias()
    {
        var provedor = ObterProvedorCompartilhado();
        Mediador = ObterMediadorDoProvedor(provedor);

    }

    public static FabricaDeDependencias Criar()
    {
        return new FabricaDeDependencias();

    }

    public static ServiceProvider ObterProvedorCompartilhado()
    {
        ServiceCollection serviceCollection = new();
        AdicionarTodasAsDependencias(serviceCollection);
        return serviceCollection.BuildServiceProvider();

    }

    private static void AdicionarTodasAsDependencias(ServiceCollection serviceCollection)
    {
        serviceCollection.AdicionarDependenciasMediatR();
        serviceCollection.CarregarAssembliesMediatR(typeof(FabricaDeDependencias).Assembly);

    }

    public static Mediador ObterMediadorDoProvedor(ServiceProvider provedor)
    {
        return provedor.GetService<Mediador>() ?? throw new System.Exception();//todo: Alterar Exception

    }

}
