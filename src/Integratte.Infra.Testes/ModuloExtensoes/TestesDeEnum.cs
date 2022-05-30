using Integratte.Infra.ModuloExtensoes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Integratte.Infra.Testes.ModuloExtensoes;

[TestClass]
public class TestesDeEnum
{
    private enum EnumTeste
    {
        Teste1 = 1,
        Teste2 = 2,
        Teste3 = 3,

    }

    [TestMethod]
    public void TesteDeConversaoDeEnum()
    {
        Enum enumTeste1 = EnumTeste.Teste1;
        var enumTeste1Convertido = enumTeste1.ConvertToEnum<EnumTeste>();
        Assert.IsTrue(enumTeste1Convertido == EnumTeste.Teste1);

    }

    [TestMethod]
    public void TesteDeTuple()
    {
        Enum enumTeste1 = EnumTeste.Teste1;
        var (id, nome) = enumTeste1.ToTuple();
        Console.WriteLine($"Id: {id} | Nome: {nome}");
        Assert.IsTrue(id == (int)EnumTeste.Teste1);

    }

    [TestMethod]
    public void ConverterStringToEnum()
    {
        var enumConvertido = "Teste1".ToEnum<EnumTeste>();
        Assert.IsTrue(enumConvertido == EnumTeste.Teste1);

    }

    [TestMethod]
    public void ListarTodasAsOpcoes()
    {
        var opcoes = ExtensoesDeEnum.ListarTudo<EnumTeste>();
        foreach (var (id, nome) in opcoes)
            Console.WriteLine($"Id: {id} | Nome: {nome}");
        Assert.IsTrue(opcoes.Length == 3);

    }

}