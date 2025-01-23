using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicAPI.Models.Entities;

[Table("TB_VENDAS")]
public class Venda
{
    [Key, Column("VEN_CODIGO")]
    public long Codigo { get; set; }

    [Column("VEN_HORARIO")]
    public TimeSpan Horario { get; set; }

    /// <summary>
    /// Seta o nome do campo e o tipo para decimal com precisão de 7,2
    /// </summary>
    [Column("VEN_VALOR_TOTAL", TypeName = "decimal(7,2)")]
    public decimal ValorTotal { get; set; }

    [Column("TB_FUNCIONARIOS_FUN_CODIGO")]
    public long FuncionarioCodigo { get; set; }

}
