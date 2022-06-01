#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using Integratte.Infra.ModuloExcecoesPersonalizadas;
using Integratte.Infra.ModuloHttp;

namespace Integratte.Infra.TestesDeApi.ModuloHttp;

internal class TokenParaApiDeTestes : TokenDaRequisicao
{
    private readonly ChamadaHttp _chamadaHttp;
    private TokenParaApiDeTestes(ChamadaHttp chamadaHttp)
    {
        _chamadaHttp = chamadaHttp;

    }

    private static TokenParaApiDeTestes _tokenParaAPIDeTestes;
    public static void Criar(ChamadaHttp chamadaHttp)
    {
        _tokenParaAPIDeTestes = new(chamadaHttp);

    }

    public static TokenParaApiDeTestes ObterInstancia()
    {
        if (_tokenParaAPIDeTestes == null)
            throw new ErroDeProgramacao("Tentativa de acesso a uma instância de Token para Api de testes sem cria-la antes.");

        return _tokenParaAPIDeTestes;

    }

    public async override Task<(string tokenDeAcesso, DateTimeOffset expiracao)> ObterNovoToken()
    {
        var requisicao = RequisicaoHttp.Criar("http://localhost:5297/", "AutenticacaoJWT/ExemploDeToken", RequisicaoHttp.MetodoHttpEnum.Get);

        var resposta = (await _chamadaHttp.Executar<RespostaTokenJwt>(requisicao))?.Resposta;
        TokenDeAcesso = resposta?.TokenJwt ?? "";
        Expiracao = resposta?.Expiracao ?? DateTimeOffset.MinValue;

        return (TokenDeAcesso, Expiracao);

    }

    private class RespostaTokenJwt
    {
        public Token Resposta { get; set; }

        public class Token
        {
            public string TokenJwt { get; set; }
            public DateTimeOffset Expiracao { get; set; }
        }

    }

}
