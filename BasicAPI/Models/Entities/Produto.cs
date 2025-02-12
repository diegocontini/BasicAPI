using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicAPI.Models.Entities;


[Table("tb_produtos")]
public class Produto
{
    [Key, Column("pro_codigo")]
    public long Codigo { get; set; }

    [Column("pro_descricao")]
    public required string Descricao { get; set; }

    [Column("pro_valor")]
    public required decimal Valor { get; set; }

    /// <summary>
    /// Nos requisitos do trabalho o campo está como inteiro, porém quantidade de produtos achei mais coerente trabalhar com decimal.
    /// </summary>
    [Column("pro_quantidade")]
    public required decimal Quantidade { get; set; }

    [Column("tb_fornecedores_for_codigo")]
    public required long FornecedorCodigo { get; set; }
}
