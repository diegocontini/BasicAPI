using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicAPI.Models.Entities;

[Table("tb_vendas")]
public class Venda
{
    [Key, Column("ven_codigo")]
    public long Codigo { get; set; }

    [Column("ven_horario")]
    public DateTime Horario { get; set; }

    /// <summary>
    /// Seta o nome do campo e o tipo para decimal com precisão de 7,2
    /// </summary>
    [Column("ven_valor_total", TypeName = "decimal(7,2)")]
    public decimal ValorTotal { get; set; }

    [Column("tb_funcionarios_fun_codigo")]
    public long FuncionarioCodigo { get; set; }

}
