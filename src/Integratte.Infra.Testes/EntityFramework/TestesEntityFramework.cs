#pragma warning disable CS8602 // Dereference of a possibly null reference.

using Integratte.Infra.EntityFramework;
using Integratte.Infra.ModuloClassesDeTipos;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Integratte.Infra.Testes.EntityFramework;

[TestClass]
public class TestesEntityFramework
{
    private const long CPF_1 = 77259188090;
    private const long CPF_2 = 8130588560;

    [TestMethod]
    public void TestarExtencoesDbCContext()
    {
        EntidadeBdCliente cliente1 = new() { CPF = CPF_1, Nome = "Fulano de Tal 1" };
        EntidadeBdCliente cliente2 = new() { Id = Guid.NewGuid(), CPF = CPF_2, Nome = "Fulano de Tal 2" };
        EntidadeBdTeste teste1 = new() { Descricao = "Teste 1" };
        EntidadeBdTeste teste2 = new() { Descricao = "Teste 2" };

        RemoverDadosAtuais();

        CriarClientesETeste1();

        ObterEAtualizarClientes();

        AdicionarTeste2EAdicionarNovoClienteATeste1();

        Filtrar();

        Incluir();

        FiltrarComInclusao();

        InclusaoEmUmaInclusao();

        #region Submétodos

        static void RemoverDadosAtuais()
        {
            using var contexto = new ContextoDoBancoDeDados();

            var testes = contexto.Listar<EntidadeBdTeste>();
            Console.WriteLine($"Removendo {testes.Count()} testes.");
            foreach (var teste in testes)
                contexto.Remover(teste);

            var clientes = contexto.Listar<EntidadeBdCliente>();
            Console.WriteLine($"Removendo {clientes.Count()} clientes.");
            foreach (var cliente in clientes)
                contexto.Remover<EntidadeBdCliente>(cliente.Id);

            contexto.Salvar();

        }

        void CriarClientesETeste1()
        {
            using var contexto = new ContextoDoBancoDeDados();

            teste1.ClientesDesteTeste.Add(new() { Cliente = cliente1 });
            contexto.Adicionar(teste1);

            contexto.AdicionarESalvar(cliente2);


        }

        void ObterEAtualizarClientes()
        {
            using var contexto = new ContextoDoBancoDeDados();
            var obter1 = contexto.ObterPorId<EntidadeBdCliente>(cliente1.Id);
            var obter2 = contexto.Obter<EntidadeBdCliente>(x => x.Id == cliente2.Id);

            obter1.Nome = "Sicrano dos Santos 1";
            contexto.Atualizar(obter1);
            obter2.Nome = "Sicrano dos Santos 2";
            contexto.AtualizarESalvar(obter2);

        }

        void AdicionarTeste2EAdicionarNovoClienteATeste1()
        {
            using var contexto = new ContextoDoBancoDeDados();

            teste2.ClientesDesteTeste.Add(new() { IdDoCliente = cliente1.Id });
            teste2.ClientesDesteTeste.Add(new() { IdDoCliente = cliente2.Id });
            contexto.Adicionar(teste2);

            contexto.AdicionarESalvar(new EntidadeBdClientesDoTeste() { IdDoCliente = cliente2.Id, IdDoTeste = teste1.Id });

        }

        void Filtrar()
        {
            using var contexto = new ContextoDoBancoDeDados();

            var lista = contexto.FiltrarEListar<EntidadeBdTeste>(x => x.ClientesDesteTeste.Any());

            Console.WriteLine("Testes com clientes:");
            foreach (var item in lista)
                Console.Write($"| {item.Descricao} ");

            Console.WriteLine("|");

        }

        void Incluir()
        {
            using var contexto = new ContextoDoBancoDeDados();

            var lista = contexto.IncluirEListar<EntidadeBdClientesDoTeste>(x => x.Cliente);

            Console.WriteLine("Clientes em testes:");
            foreach (var item in lista.Select(x => x.Cliente).Distinct())
                Console.Write($"| {item.Nome} - {CPF.Criar(item.CPF)} ");

            Console.WriteLine("|");

        }

        void FiltrarComInclusao()
        {
            using var contexto = new ContextoDoBancoDeDados();

            var cpf = CPF.Criar(CPF_2);

            var lista = contexto.FiltrarComInclusaoEListar<EntidadeBdTeste>(x => x.ClientesDesteTeste.Any(y => y.Cliente.CPF == cpf.Numero), x => x.ClientesDesteTeste);

            Console.WriteLine($"Testes com cliente de cpf {cpf}: ");
            foreach (var item in lista)
                Console.Write($"| {item.Descricao} com {item.ClientesDesteTeste.Count} cliente(s). ");

            Console.WriteLine("|");

        }

        void InclusaoEmUmaInclusao()
        {
            using var contexto = new ContextoDoBancoDeDados();

            var lista = contexto.Tabela<EntidadeBdTeste>()
                                                    .Include(x => x.ClientesDesteTeste)
                                                    .ThenInclude(x => x.Cliente)
                                                    .ToList();

            foreach (var item in lista)
            {
                Console.WriteLine($"Clientes do {item.Descricao} ");
                foreach (var cliente in item.ClientesDesteTeste)
                    Console.Write($"| {cliente.Cliente.Nome} - {CPF.Criar(cliente.Cliente.CPF)} ");
                Console.WriteLine("|");

            }

        }

        #endregion

    }

}