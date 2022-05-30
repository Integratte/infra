using Integratte.Infra.ModuloExtensoes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Integratte.Infra.Testes.ModuloExtensoes;

[TestClass]
public class TestesDeException
{
    [TestMethod]
    public void TestarInnerException()
    {
        var exception1 = new Exception("Exception 1");
        var exception2 = new Exception("Exception 2", exception1);
        var exception3 = new Exception("Exception 3", exception2);

        string result = exception3.TextoAteExceptionRaiz();
        Assert.IsTrue(result == "Exception 3 -> Exception 2 -> Exception 1");

    }

}