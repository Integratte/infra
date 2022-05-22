using Integratte.Infra.Testes.Fabricas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Integratte.Infra.Testes.ModuloMediador;

[TestClass]
public class TestesDeNotificacoes
{
    [TestMethod]
    public void PossoAdicionarUmaNotificacaoComApenasUmaMensagem()
    {
        var mediador = FabricaDeDependencias.Criar().Mediador;
        mediador.AdicionarNotificacao("Teste de Adição");

        //Deve possuir uma notificação.
        Assert.IsTrue(mediador.Notificacoes.Length == 1);

    }

    [TestMethod]
    public async Task UmaNotificacaoQueProvocaInterrupcaoNaoDevePermitirMaisAcoesNoMediador()
    {
        var mediador = FabricaDeDependencias.Criar().Mediador;
        mediador.AdicionarNotificacao("Notificação que provoca interrupção", exibirParaUsuario: false, requisicaoInvalida: true, provocaInterrupcaoDoSistema: true);

        //Tentativa de publicar evento, o mesmo não deve ocorrer.
        Assert.IsFalse(await TesteDeEvento.Testar(mediador));

    }

    [TestMethod]
    public void TodasAsNotificacoesDoMediadorPrecisamSerConsultadasPosteriormente()
    {
        var provedor = FabricaDeDependencias.ObterProvedorCompartilhado();

        var mediador1 = FabricaDeDependencias.ObterMediadorDoProvedor(provedor);
        mediador1.AdicionarNotificacao("Adição da primeira notificação");

        var mediador2 = FabricaDeDependencias.ObterMediadorDoProvedor(provedor);
        mediador2.AdicionarNotificacao("Adição da segunda notificação");

        //Uma nova injeção deve conter as notificações anteriores.
        var mediador3 = FabricaDeDependencias.ObterMediadorDoProvedor(provedor);
        Assert.IsTrue(mediador3.Notificacoes.Length == 2);

    }

}
