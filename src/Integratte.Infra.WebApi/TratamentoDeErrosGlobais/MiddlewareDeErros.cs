using Integratte.Infra.ModuloExcecoes;
using Integratte.Infra.ModuloMediador;
using Integratte.Infra.WebApi.Controller;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Integratte.Infra.WebApi.TratamentoDeErrosGlobais;

public class MiddlewareDeErros
{
    private const string CodigoDeErro = "[Erro_WebApi]";
    private readonly RequestDelegate _next;
    private readonly ILogger<ControllerApiBase> _logger;

    public MiddlewareDeErros(RequestDelegate next, ILogger<ControllerApiBase> logger)
    {
        _next = next;
        _logger = logger;

    }

    public async Task Invoke(HttpContext context, Mediador mediador)
    {
        try { await _next(context); }
        catch (Exception ex) { await TratarExcecao(context, ex, mediador); }

    }

    private Task TratarExcecao(HttpContext context, Exception ex, Mediador mediador)
    {
        ErroDTO erro;

        CriarErro();

        NotificarELogar();

        return RetornarResposta();

        #region Submétodos

        void CriarErro()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (environment != "Development")
                erro = new ErroDTO($"Tivemos um problema, por favor, verique os logs.");
            else
                erro = new ErroDTO(ex.InnerExceptionRaiz(), ex);

        }

        void NotificarELogar()
        {
            var referencia = $"Ref.: {erro.Id}";
            mediador.AdicionarNotificacao($"{erro.Mensagem} - {referencia}", exibirParaUsuario: false);

            string erroDetalhado = $"{CodigoDeErro} - {referencia} - {ex.InnerExceptionRaiz()}";
            _logger.LogError(erroDetalhado);

        }

        Task RetornarResposta()
        {
            context.Response.StatusCode = 500; //Internal Server Error
            context.Response.ContentType = "application/problem+json";
            return context.Response.WriteAsync(JsonConvert.SerializeObject(new RetornoPadraoDaApi(mediador.Notificacoes, sucedido: false)));

        }

        #endregion

    }

    private class ErroDTO
    {
        public ErroDTO(string mensagem, Exception? ex = null)
        {
            Excecao = ex;
            Mensagem = mensagem;

        }

        public Guid Id { get; private set; } = Guid.NewGuid();
        public Exception? Excecao { get; private set; }
        public string Mensagem { get; private set; }


    }

}
