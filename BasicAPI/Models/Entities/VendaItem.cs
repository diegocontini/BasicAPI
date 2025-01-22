using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicAPI.Models.Entities;

[Table("TB_ITENS")]
public class VendaItem
{
    [Key, Column("ITE_CODIGO")]
    public long Codigo { get; set; }

    /// <summary>
    /// Nos requisitos do trabalho o campo está como inteiro, porém quantidade de produtos achei mais coerente trabalhar com decimal.
    /// </summary>
    [Column("ITE_QUANTIDADE")]
    public required decimal Quantidade { get; set; }

    [Column("ITE_VALOR_PRODUTOS")]
    public required decimal Valor { get; set; }

    [Column("TB_PRODUTOS_PRO_CODIGO")]
    public required long ProdutoCodigo { get; set; }

    [Column("TB_VENDAS_VEN_CODIGO")]
    public required long VendaCodigo { get; set; }

}
