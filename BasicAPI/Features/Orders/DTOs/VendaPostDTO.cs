namespace BasicAPI.Features.Orders.DTOs;






public class VendaPostDTO
{
    public DateTime Horario { get; set; }   
    public decimal ValorTotal { get; set; }
    public long FuncionarioCodigo { get; set; }
    public required IEnumerable<VendaItemPostDTO> Itens { get; set; }


}

