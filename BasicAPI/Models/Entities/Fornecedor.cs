using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicAPI.Models.Entities;

[Table("TB_FORNECEDORES")]
public class Fornecedor
{

    [Key, Column("FOR_CODIGO")]
    public long Codigo { get; set; }

    [Column("FOR_DESCRICAO")]
    required public string Descricao { get; set; }

}
