namespace Integratte.Infra.ModuloMediador.Comandos;

public interface IComandosDoMediador
{
    Task Executar(IComando comando);
    Task<RetornoT?> Executar<RetornoT>(IComando<RetornoT> comando) where RetornoT : IRetornoDoComando;

}