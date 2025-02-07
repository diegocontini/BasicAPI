using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BasicAPI.Features.Orders.DTOs;

public class VendaGetDTO
{
    public long Codigo { get; set; }
    public TimeSpan Horario { get; set; }
    public decimal ValorTotal { get; set; }
    public long FuncionarioCodigo { get; set; }
}
