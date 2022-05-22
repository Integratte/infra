using Integratte.Infra.Testes.Fabricas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Integratte.Infra.Testes.ModuloMediador;

[TestClass]
public class TestesDeConsultas
{
    [TestMethod]
    public async Task TestarConsulta()
    {
        var mediador = FabricaDeDependencias.Criar().Mediador;
        Assert.IsTrue(await TesteDeConsulta.Testar(mediador));

    }

}