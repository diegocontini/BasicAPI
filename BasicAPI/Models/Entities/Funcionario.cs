using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicAPI.Models.Entities;

[Table("tb_funcionarios")]
public class Funcionario
{
    [Key, Column("fun_codigo")]
    public long Codigo { get; set; }

    [Column("fun_nome")]
    public required string Nome { get; set; }

    [Column("fun_cpf")]
    public required string CPF { get; set; }

    [Column("fun_senha")]
    public required string Senha { get; set; }

    [Column("fun_funcao")]
    public required string Funcao { get; set; }
}
