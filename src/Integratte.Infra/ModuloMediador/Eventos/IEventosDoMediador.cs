namespace Integratte.Infra.ModuloMediador.Eventos;

public interface IEventosDoMediador
{
    Task Publicar(IEvento evento);

}