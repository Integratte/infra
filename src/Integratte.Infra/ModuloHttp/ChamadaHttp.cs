using Integratte.Infra.ModuloExcecoesPersonalizadas;
using Newtonsoft.Json;

namespace Integratte.Infra.ModuloHttp;

public abstract class ChamadaHttp
{
    protected RequisicaoHttp RequisicaoHttp { get; private set; }
    public string? ConteudoQuandoOcorreErro { get; protected set; }

    public async Task ExecutarAsync(RequisicaoHttp requisicao)
    {
        RequisicaoHttp = requisicao;
        await ExecutarChamadaAsync();


    }
    public async Task<T?> Executar<T>(RequisicaoHttp requisicao)
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
