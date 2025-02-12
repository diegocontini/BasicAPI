using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicAPI.Models.Entities;

[Table("tb_itens")]
public class VendaItem
{
    [Key, Column("ite_codigo")]
    public long Codigo { get; set; }

    /// <summary>
    /// Nos requisitos do trabalho o campo está como inteiro, porém quantidade de produtos achei mais coerente trabalhar com decimal.
    /// </summary>
    [Column("ite_quantidade")]
    public required decimal Quantidade { get; set; }

    [Column("ite_valor_produtos")]
    public required decimal Valor { get; set; }

    [Column("tb_produtos_pro_codigo")]
    public required long ProdutoCodigo { get; set; }

    [Column("tb_vendas_ven_codigo")]
    public required long VendaCodigo { get; set; }

}
