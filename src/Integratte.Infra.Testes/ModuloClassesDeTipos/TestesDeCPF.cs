using Integratte.Infra.ModuloClassesDeTipos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Integratte.Infra.Testes.ModuloClassesDeTipos;

[TestClass]
public class TestesDeCPF
{
    [TestMethod]
    public void TesteDeFormatacao()
    {
        var cpf = CPF.Criar(8130588560);
        Assert.IsTrue("081.305.885-60" == cpf.Texto);

    }

    [TestMethod]
    public void CriarPorTexto()
    {
        var cpf = CPF.Criar("081.305.885-60");
        Assert.IsTrue(cpf.Valido);

    }

    [TestMethod]
    public void CriarPorTextoDeNumeros()
    {
        var cpf = CPF.Criar("08130588560");
        Assert.IsTrue(cpf.Valido);

    }

    [TestMethod]
    public void TesteDeIgualdade()
    {
        var cpfPorString = CPF.Criar("08130588560");
        var cpfPorLong = CPF.Criar(8130588560);

        Assert.IsTrue(cpfPorString == cpfPorLong);

    }

}