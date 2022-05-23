namespace Integratte.Infra.ExcecoesPersonalizadas;

public class ErroDeProgramacao : Exception
{
    public ErroDeProgramacao(string mensagem, Exception? inner = null) : base(mensagem, inner) { }

}
