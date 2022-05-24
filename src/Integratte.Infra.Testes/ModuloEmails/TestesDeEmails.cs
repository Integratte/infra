using Integratte.Infra.Testes.Fabricas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace Integratte.Infra.Testes.ModuloEmails;

[TestClass]
public class TestesDeEmails
{
    [TestMethod]
    public async Task TestarEnvioDeEmailAsync()
    {
        var notificacoesDeEmail = await FabricaDeDependencias.Criar().EnvioDeEmail
                        .InformarDestinatario("fillippeprata@test.com")
                        .InformarAssunto("Teste de Envio de E-mail Integratte.Infra")
                        .InformarCorpo("Obrigado por me testar $TokenDoUsuario$. Saiba mais sobre mim em https://github.com/Integratte/infra.")
                        .InformarTokensParaSubstituir(new() { { "$TokenDoUsuario$", "Fillippe Prata" } })
                        .EnviarEmailAsync();

        Assert.IsTrue(notificacoesDeEmail.Length == 0);

    }

    [TestMethod]
    public async Task NecessarioInformarAoMenosUmEnderecoDeEmailAsync()
    {
        var retornoDoEnvioDeEmail = await FabricaDeDependencias.Criar().EnvioDeEmail
                                    .InformarDestinatarios(System.Array.Empty<string>())
                                    .EnviarEmailAsync();

        Assert.IsTrue(retornoDoEnvioDeEmail.Any(x => x.Mensagem == "Necessário informar ao menos um endereço de e-mail."));

    }

    [TestMethod]
    public async Task NaoPodeHaverEnderecosDeEmailEmBrancoAsync()
    {
        var retornoDoEnvioDeEmail = await FabricaDeDependencias.Criar().EnvioDeEmail
                                    .InformarDestinatario("")
                                    .EnviarEmailAsync();

        Assert.IsTrue(retornoDoEnvioDeEmail.Any(x => x.Mensagem == "Existem elementos vazios na lista de endereço de e-mail."));

    }

    [TestMethod]
    public async Task NessarioInformarOAssuntoAsync()
    {
        var retornoDoEnvioDeEmail = await FabricaDeDependencias.Criar().EnvioDeEmail
                                    .InformarAssunto("")
                                    .EnviarEmailAsync();

        Assert.IsTrue(retornoDoEnvioDeEmail.Any(x => x.Mensagem == "Necessário informar o assunto do e-mail."));

    }

    [TestMethod]
    public async Task NessarioInformarOCorpoAsync()
    {
        var retornoDoEnvioDeEmail = await FabricaDeDependencias.Criar().EnvioDeEmail
                                    .InformarAssunto("")
                                    .EnviarEmailAsync();

        Assert.IsTrue(retornoDoEnvioDeEmail.Any(x => x.Mensagem == "Necessário informar o corpo do e-mail."));

    }

}