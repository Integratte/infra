#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using Integratte.Infra.ModuloExcecoesPersonalizadas;
using Newtonsoft.Json;

namespace Integratte.Infra.ModuloHttp;

public abstract class ChamadaHttp
{
    protected RequisicaoHttp RequisicaoHttp { get; private set; }
    public string? ConteudoQuandoOcorreErro { get; protected set; }

    public void Executar(RequisicaoHttp requisicao)
    {
        Task.FromResult(ExecutarAsync(requisicao));

    }

    public T? Executar<T>(RequisicaoHttp requisicao)
    {
        return ExecutarAsync<T>(requisicao).Result;

    }

    public async Task ExecutarAsync(RequisicaoHttp requisicao)
    {
        RequisicaoHttp = requisicao;
        await ExecutarChamadaAsync();


    }

    public async Task<T?> ExecutarAsync<T>(RequisicaoHttp requisicao)
    {
        RequisicaoHttp = requisicao;
        var (sucesso, conteudo) = await ExecutarChamadaAsync();

        if (sucesso)
        {
            try
            {
                T? resultado = JsonConvert.DeserializeObject<T>(conteudo ?? "");
                return resultado;

            }
            catch (Exception ex) { throw new ErroDeRequisicaoHttp("Problema ao converter resposta de chamada Http", ex); }

        }

        return default;


    }

    protected abstract Task<(bool sucesso, string conteudo)> ExecutarChamadaAsync();

}
