#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using Integratte.Infra.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Integratte.Infra.Testes.EntityFramework;

[Table(nameof(ContextoDoBancoDeDados.ClientesDoTeste), Schema = ContextoDoBancoDeDados.Schema)]
public class EntidadeBdClientesDoTeste : EntidadeDoBancoDeDados
{
    [Key]
    public override Guid Id { get; set; } = Guid.NewGuid();
    public DateTimeOffset CadastradoEm { get; set; } = DateTimeOffset.Now;

    public Guid IdDoCliente { get; set; }
    public EntidadeBdCliente Cliente { get; set; }

    public Guid IdDoTeste { get; set; }
    public EntidadeBdTeste Teste { get; set; }

}
