#pragma warning disable CS8602 // Dereference of a possibly null reference.

using Integratte.Infra.EntityFramework;
using Integratte.Infra.ModuloClassesDeTipos;
using Integratte.Infra.Testes.Fabricas;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Integratte.Infra.Testes.Cache;

[TestClass]
public class TestesCacheEmMemoria
{
    private readonly string chave1 = "Chave1";
    private string CriacaoDeObjetoCacheado() => "ObjetoCacheado";

    [TestMethod]
    public void VerificarCacheEmMemoria()
    {
        var cacheEmMemoria = FabricaDeDependencias.Criar().CacheEmMemoria;


        cacheEmMemoria.CriarCache(chave1, CriacaoDeObjetoCacheado);
        var obtendoChave = cacheEmMemoria.ObterSeExistir<string>(chave1);

        Assert.AreEqual(obtendoChave, CriacaoDeObjetoCacheado());

        cacheEmMemoria.Remover(chave1);

        obtendoChave = cacheEmMemoria.ObterSeExistir<string>(chave1);
        Assert.IsNull(obtendoChave);

        #region Submétodos

        #endregion

    }


}