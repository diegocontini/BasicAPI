using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicAPI.Models.Entities;

[Table("tb_fornecedores")]
public class Fornecedor
{

    [Key, Column("for_codigo")]
    public long Codigo { get; set; }

    [Column("for_descricao")]
    required public string Descricao { get; set; }

}
