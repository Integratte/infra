#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using Microsoft.EntityFrameworkCore;
using System;

namespace Integratte.Infra.Testes.EntityFramework;

public class ContextoDoBancoDeDados : DbContext
{
    public const string Schema = "TestesInfra";

    public ContextoDoBancoDeDados() : base(GetOptions("Server=(localdb)\\mssqllocaldb;Database=Integratte_Infra_Testes;Trusted_Connection=True;")) { Inicializar(); }

    public ContextoDoBancoDeDados(string connectionString) : base(GetOptions(connectionString)) { Inicializar(); }

    public ContextoDoBancoDeDados(DbContextOptions<ContextoDoBancoDeDados> options) : base(options) { Inicializar(); }

    private void Inicializar()
    {
        ChangeTracker.LazyLoadingEnabled = false;

    }

    private static DbContextOptions GetOptions(string connectionString)
    {
        return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("dbo");

        modelBuilder.Entity<EntidadeBdTeste>().HasMany(x => x.ClientesDesteTeste).WithOne(x => x.Teste).HasForeignKey(x => x.IdDoTeste).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<EntidadeBdCliente>().HasMany(x => x.TestesDesteCliente).WithOne(x => x.Cliente).HasForeignKey(x => x.IdDoCliente).OnDelete(DeleteBehavior.Cascade);


    }

    public DbSet<EntidadeBdTeste> Testes { get; set; }
    public DbSet<EntidadeBdClientesDoTeste> ClientesDoTeste { get; set; }
    public DbSet<EntidadeBdCliente> Clientes { get; set; }

}
