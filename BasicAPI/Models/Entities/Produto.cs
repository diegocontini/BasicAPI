using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicAPI.Models.Entities;


[Table("TB_PRODUTOS")]
public class Produto
{
    [Key, Column("PRO_CODIGO")]
    public long Codigo { get; set; }

    [Column("PRO_DESCRICAO")]
    public required string Descricao { get; set; }

    [Column("PRO_VALOR")]
    public required decimal Valor { get; set; }

    /// <summary>
    /// Nos requisitos do trabalho o campo está como inteiro, porém quantidade de produtos achei mais coerente trabalhar com decimal.
    /// </summary>
    [Column("PRO_QUANTIDADE")]
    public required decimal Quantidade { get; set; }

    [Column("TB_FORNECEDORES_FOR_CODIGO")]
    public required long FornecedorCodigo { get; set; }
}
