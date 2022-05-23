namespace Integratte.Infra.ModuloExcecoesPersonalizadas;

public class ErroDeProgramacao : Exception
{
    public ErroDeProgramacao(string mensagem, Exception? inner = null) : base(mensagem, inner) { }

}
