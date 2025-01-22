using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicAPI.Models.Entities;

[Table("TB_FUNCIONARIOS")]
public class Funcionario
{
    [Key, Column("FUN_CODIGO")]

    public long Codigo { get; set; }

    [Column("FUN_NOME")]
    public required string Nome { get; set; }

    [Column("FUN_CPF")]
    public required string CPF { get; set; }

    [Column("FUN_SENHA")]
    public required string Senha { get; set; }

    [Column("FUN_FUNCAO")]
    public required string Funcao { get; set; }



}
