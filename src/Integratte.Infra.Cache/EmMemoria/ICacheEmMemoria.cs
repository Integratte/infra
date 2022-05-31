namespace Integratte.Infra.Cache.EmMemoria;

public interface ICacheEmMemoria
{
    ObjetoT? ObterSeExistir<ObjetoT>(string chave);
    ObjetoT CriarCache<ObjetoT>(string chave, Func<ObjetoT> funcaoDeCriacaoDoObjeto, TimeSpan? expiraEm = null);
    ObjetoT ObterOuCriarSeNaoExistir<ObjetoT>(string chave, Func<ObjetoT> funcaoDeCriacaoDoObjeto, TimeSpan expiraEm);
    void Remover(string chave);

}
