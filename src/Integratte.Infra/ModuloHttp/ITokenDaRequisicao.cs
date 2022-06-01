#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.

using Integratte.Infra.ModuloExtensoes;

namespace Integratte.Infra.ModuloHttp;

public abstract class TokenDaRequisicao
{
    protected string? TokenDeAcesso { get; set; }
    protected DateTimeOffset Expiracao { get; set; }

    public abstract Task<(string tokenDeAcesso, DateTimeOffset expiracao)> ObterNovoToken();
    public async Task<(string tokenDeAcesso, DateTimeOffset expiracao)> ObterTokenAtual()
    {
        if (DateTimeOffset.Now < Expiracao && TokenDeAcesso.ContemValor())
            return (TokenDeAcesso, Expiracao);

        return await ObterNovoToken();

    }



}
