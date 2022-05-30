using Integratte.Infra.ModuloExtensoes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Integratte.Infra.Testes.ModuloExtensoes;

[TestClass]
public class TestesDeString
{
    [TestMethod]
    public void TestarNuloOuVazio()
    {
        Assert.IsTrue("".NuloOuVazio());

        string? teste = null;
        Assert.IsTrue(teste.NuloOuVazio());

    }

    [TestMethod]
    public void TestarContemValor()
    {
        Assert.IsTrue("123".ContemValor());

    }

    [TestMethod]
    public void TestarSomenteLetrasOuNumeros()
    {
        Assert.IsTrue("asji21kl12" == "a.s,j!i2@#1kl12".SomenteLetrasOuNumeros());

    }

    [TestMethod]
    public void TestarSomenteNumeros()
    {
        Assert.IsTrue("1234" == "1a.s,j!i2@#3kl4".SomenteNumeros());

    }

}