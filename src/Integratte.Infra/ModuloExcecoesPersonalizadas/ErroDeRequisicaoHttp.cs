namespace Integratte.Infra.ModuloExcecoesPersonalizadas;

public class ErroDeRequisicaoHttp : Exception
{
    public ErroDeRequisicaoHttp(string mensagem, Exception? inner = null) : base(mensagem, inner) { }

}
