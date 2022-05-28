#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using Integratte.Infra.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Integratte.Infra.Testes.EntityFramework;

[Table(nameof(ContextoDoBancoDeDados.Clientes), Schema = ContextoDoBancoDeDados.Schema)]
[Index(nameof(CPF), IsUnique = true, Name = "IX_ExemploChaveUnica")]
public class EntidadeBdCliente : EntidadeDoBancoDeDados
{
    [Key]
    public override Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; }
    public ulong CPF { get; set; }
    public ICollection<EntidadeBdClientesDoTeste> TestesDesteCliente { get; set; }

}
