using MediatR;

namespace Integratte.Infra.ModuloMediador.Comandos;
public abstract class Comando : IComando, INotification { }
public abstract class Comando<RetornoT> : IComando<RetornoT>, IRequest<RetornoT> where RetornoT : IRetornoDoComando { }
