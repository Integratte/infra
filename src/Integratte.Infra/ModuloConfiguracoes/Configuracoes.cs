using Integratte.Infra.ModuloExtensoes;
using Microsoft.Extensions.Configuration;

namespace Integratte.Infra.ModuloConfiguracoes;

public class Configuracoes : IConfiguracoes
{
    private readonly IConfiguration _configuration;

    public Configuracoes(IConfiguration configuration)
    {
        _configuration = configuration;

    }

    private string _pastaDeConfiguracoes = "";
    public string PastaDeConfiguracoes
    {
        get
        {
            if (_pastaDeConfiguracoes.NuloOuVazio())
            {
                var appConfig = _configuration["AppConfigRaiz"];
                if (appConfig.ContemValor())
                    _pastaDeConfiguracoes = appConfig;

            }

            return _pastaDeConfiguracoes;

        }

    }

}
