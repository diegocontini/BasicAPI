using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BasicAPI.Features.Orders.DTOs;

public class VendaItemPostDTO
{

    public required decimal Quantidade { get; set; }

    public required decimal Valor { get; set; }

    public required long ProdutoCodigo { get; set; }

    
}


