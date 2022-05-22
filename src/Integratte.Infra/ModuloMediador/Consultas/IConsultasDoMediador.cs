namespace Integratte.Infra.ModuloMediador.Consultas;

public interface IConsultasDoMediador
{
    Task<RetornoT?> Consultar<RetornoT>(IConsulta<RetornoT> consulta) where RetornoT : IRetornoDaConsulta;

}