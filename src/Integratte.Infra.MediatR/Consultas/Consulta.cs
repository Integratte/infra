using MediatR;

namespace Integratte.Infra.ModuloMediador.Consultas;
public abstract class Consulta<RetornoT> : IConsulta<RetornoT>, IRequest<RetornoT> where RetornoT : IRetornoDaConsulta { }
