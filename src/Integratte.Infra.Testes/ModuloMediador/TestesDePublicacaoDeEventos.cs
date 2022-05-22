using Integratte.Infra.Testes.Fabricas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Integratte.Infra.Testes.ModuloMediador;

[TestClass]
public class TestesDePublicacaoDeEventos
{
    [TestMethod]
    public async Task VerificarSeEventoFoiPublicado()
    {
        var mediador = FabricaDeDependencias.Criar().Mediador;
        Assert.IsTrue(await TesteDeEvento.Testar(mediador));

    }

}
