using Integratte.Infra.ModuloClassesDeTipos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Integratte.Infra.Testes.ModuloClassesDeTipos;

[TestClass]
public class TestesDeCNPJ
{
    [TestMethod]
    public void TesteDeFormatacao()
    {
        var cnpj = CNPJ.Criar(1395189000121);
        Assert.IsTrue("01.395.189/0001-21" == cnpj.Texto);

    }

    [TestMethod]
    public void CriarPorTexto()
    {
        var cnpj = CNPJ.Criar("01.395.189/0001-21");
        Assert.IsTrue(cnpj.Valido);

    }

    [TestMethod]
    public void CriarPorTextoDeNumeros()
    {
        var cnpj = CNPJ.Criar("01395189000121");
        Assert.IsTrue(cnpj.Valido);

    }

    [TestMethod]
    public void TesteDeIgualdade()
    {
        var cpfPorString = CNPJ.Criar("01395189000121");
        var cpfPorLong = CNPJ.Criar(1395189000121);

        Assert.IsTrue(cpfPorString == cpfPorLong);

    }

}