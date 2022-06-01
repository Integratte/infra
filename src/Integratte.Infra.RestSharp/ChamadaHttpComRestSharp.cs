using Integratte.Infra.ModuloExcecoesPersonalizadas;
using Integratte.Infra.ModuloExtensoes;
using Integratte.Infra.ModuloHttp;
using RestSharp;
using System.Net;
using static Integratte.Infra.ModuloHttp.RequisicaoHttp;

namespace Integratte.Infra.RestSharp;

internal class ChamadaHttpComRestSharp : ChamadaHttp
{
    protected async override Task<(bool sucesso, string conteudo)> ExecutarChamadaAsync()
    {
        var request = await CarregarParametrosAsync();
        var client = CarregarRestClient(request);
        var result = await client.ExecuteAsync(request);

        //Provavelmente o token expirou, tentar novamente com novo token:
        if (result.StatusCode == HttpStatusCode.Unauthorized)
        {
            request = await CarregarParametrosAsync(novoToken: true);
            client = CarregarRestClient(request);
            result = await client.ExecuteAsync(request);

        }

        if (!result.IsSuccessful)
            LancarErro(result);

        return (result.IsSuccessful, result.Content);

    }

    private RestClient CarregarRestClient(RestRequest request)
    {
        RestClient client;
        if (RequisicaoHttp.ProxyUrl.NuloOuVazio())
            client = new RestClient(RequisicaoHttp.HostUrl);
        else
        {
            client = new RestClient(RequisicaoHttp.HostUrl)
            {
                Proxy = new WebProxy(RequisicaoHttp.ProxyUrl)

            };
            client.Proxy.Credentials = CredentialCache.DefaultCredentials;

        }

        request.Timeout = client.ReadWriteTimeout = request.ReadWriteTimeout = client.Timeout = RequisicaoHttp.TimeOut;

        return client;

    }

    private async Task<RestRequest> CarregarParametrosAsync(bool novoToken = false)
    {
        ServicePointManager.SecurityProtocol = RequisicaoHttp.ProtocoloDeSeguranca;

        var request = new RestRequest(RequisicaoHttp.Rota, RequisicaoHttp.MetodoHttp.ConvertToEnum<Method>());

        if (RequisicaoHttp.PDFs?.Count > 0)
        {
            request.AlwaysMultipartFormData = true;
            foreach (var pdf in RequisicaoHttp.PDFs)
                request.AddFileBytes(pdf.NomeDoParametro, pdf.Bytes, pdf.NomeDoArquivo, ParametroPDF.TipoDeConteudoPDF);

        }

        if (RequisicaoHttp.ValoresDoCabecalho?.Count > 0)
            foreach (var header in RequisicaoHttp.ValoresDoCabecalho)
                request.AddHeader(header.Key, header.Value);

        if (RequisicaoHttp.ParametrosHttp?.Count > 0)
            foreach (var parameter in RequisicaoHttp.ParametrosHttp)
                request.AddParameter(parameter.Key, parameter.Value);

        if (RequisicaoHttp.ParametrosQueryString?.Count > 0)
            foreach (var queryString in RequisicaoHttp.ParametrosQueryString)
                request.AddParameter(queryString.Key, queryString.Value, ParameterType.QueryString);

        if (RequisicaoHttp.ITokenDaRequisicao != null)
        {
            var (token, _) = novoToken ? await RequisicaoHttp.ITokenDaRequisicao.ObterNovoToken() : await RequisicaoHttp.ITokenDaRequisicao.ObterTokenAtual();
            if (token.ContemValor())
                request.AddHeader("Authorization", $"Bearer {token}");

        }

        request.RequestFormat = DataFormat.Json;
        if (RequisicaoHttp.Conteudo != null)
            request.AddJsonBody(RequisicaoHttp.Conteudo);

        return request;

    }

    private void LancarErro(IRestResponse response)
    {
        ConteudoQuandoOcorreErro = response.Content.NuloOuVazio() ? response.ErrorMessage : response.Content;
        var rota = $"Rota: {RequisicaoHttp.Rota}";
        var codigoDoStatus = ((int)response.StatusCode) == 0 ? "" : $" | Código do status: {response.StatusCode}.";
        var resposta = (response.Content.NuloOuVazio() ? "" : " | Resposta: " + response.Content);
        var mensagemDeErro = (response.ErrorMessage.NuloOuVazio() ? "" : " | Erro: " + response.ErrorMessage);
        throw new ErroDeRequisicaoHttp($"Erro em chamada Http => {rota}{codigoDoStatus}{resposta}{mensagemDeErro}");

    }

}
