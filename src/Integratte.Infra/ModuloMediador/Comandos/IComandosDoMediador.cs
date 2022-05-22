namespace Integratte.Infra.ModuloMediador.Comandos;

public interface IComandosDoMediador
{
    Task Executar(IComando comando);

}