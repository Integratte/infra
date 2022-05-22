using Integratte.Infra.Testes.Fabricas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Integratte.Infra.Testes.ModuloMediador;

[TestClass]
public class TestesDeExecucaoDeComandos
{
    [TestMethod]
    public async Task VerificarSeComandoSemRetornoFoiExecutado()
    {
        var mediador = FabricaDeDependencias.Criar().Mediador;
        Assert.IsTrue(await TesteDeComandoSemRetorno.Testar(mediador));

    }

    [TestMethod]
    public async Task VerificarSeComandoComRetornoFoiExecutado()
    {
        var mediador = FabricaDeDependencias.Criar().Mediador;
        Assert.IsTrue(await TesteDeComandoComRetorno.Testar(mediador));

    }

}
