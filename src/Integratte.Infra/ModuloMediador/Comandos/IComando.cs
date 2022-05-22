namespace Integratte.Infra.ModuloMediador.Comandos
{
    public interface IComando { }
    public interface IComando<RetornoT> where RetornoT : IRetornoDoComando { }

}
