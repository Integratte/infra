using System.Runtime.Caching;

namespace Integratte.Infra.Cache.EmMemoria;

public class CacheComMemoryCache : ICacheEmMemoria
{
    private static readonly MemoryCache CacheEmMemoria = new("IntegratteMemoryCache");

    public ObjetoT? ObterSeExistir<ObjetoT>(string chave)
    {
        if (CacheEmMemoria.Contains(chave))
            return (ObjetoT)CacheEmMemoria.Get(chave);

        return default;

    }

    public ObjetoT CriarCache<ObjetoT>(string chave, Func<ObjetoT> funcaoDeCriacaoDoObjeto, TimeSpan? expiraEm)
    {
        var objetoT = ObterSeExistir<ObjetoT>(chave);
        if (objetoT == null)
        {
            objetoT = funcaoDeCriacaoDoObjeto();
            CacheItemPolicy cacheItemPolicy = new()
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow + (expiraEm ?? new TimeSpan(1, 0, 0, 0))

            };

            CacheEmMemoria.Add(chave, objetoT, cacheItemPolicy);

            return objetoT;

        }
        else
        {
            CacheEmMemoria.Remove(chave);
            return CriarCache(chave, funcaoDeCriacaoDoObjeto, expiraEm);

        }

    }

    public ObjetoT ObterOuCriarSeNaoExistir<ObjetoT>(string chave, Func<ObjetoT> funcaoDeCriacaoDoObjeto, TimeSpan expiraEm)
    {
        var objetoT = ObterSeExistir<ObjetoT>(chave);
        if (objetoT == null)
            return CriarCache(chave, funcaoDeCriacaoDoObjeto, expiraEm);
        else
            return objetoT;

    }

    public void Remover(string chave)
    {
        CacheEmMemoria.Remove(chave);

    }

}