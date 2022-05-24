using Integratte.Infra.MediatR;
using Integratte.Infra.ModuloEmails;
using Integratte.Infra.ModuloExcecoesPersonalizadas;
using Integratte.Infra.ModuloMediador;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Integratte.Infra.Testes.Fabricas;

internal class FabricaDeDependencias
{
    public Mediador Mediador { get; private set; }
    public EnvioDeEmail EnvioDeEmail { get; private set; }

    private FabricaDeDependencias()
    {
        var provedor = ObterProvedorCompartilhado();
        EnvioDeEmail = ObterEnvioDeEmailDoProvedor();
        Mediador = ObterMediadorDoProvedor(provedor);

        #region SubMétodos

        EnvioDeEmail ObterEnvioDeEmailDoProvedor()
        {
            return provedor.GetService<EnvioDeEmail>() ?? throw new ErroDeProgramacao("Não foi configurado uma injeção para o EnvioDeEmail.");

        }

        #endregion

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
        var configuration = ConfigurationMock.Criar();
        serviceCollection.AddSingleton(s => configuration);
        serviceCollection.AdicionarDependenciasInfra();
        serviceCollection.AdicionarDependenciasMediatR();
        serviceCollection.CarregarAssembliesMediatR(typeof(FabricaDeDependencias).Assembly);

    }

    public static Mediador ObterMediadorDoProvedor(ServiceProvider provedor)
    {
        return provedor.GetService<Mediador>() ?? throw new ErroDeProgramacao("Não foi configurado uma injeção para o Mediador.");

    }

    public static class ConfigurationMock
    {
        public static IConfiguration Criar()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            return config;

        }

    }

}
