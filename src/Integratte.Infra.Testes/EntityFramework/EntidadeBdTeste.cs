#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using Integratte.Infra.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Integratte.Infra.Testes.EntityFramework;

[Table(nameof(ContextoDoBancoDeDados.Testes), Schema = ContextoDoBancoDeDados.Schema)]
public class EntidadeBdTeste : EntidadeDoBancoDeDados
{
    public EntidadeBdTeste()
    {
        ClientesDesteTeste = new List<EntidadeBdClientesDoTeste>();

    }

    [Key]
    public override Guid Id { get; set; } = Guid.NewGuid();
    public string Descricao { get; set; }
    public ICollection<EntidadeBdClientesDoTeste> ClientesDesteTeste { get; set; }

}
