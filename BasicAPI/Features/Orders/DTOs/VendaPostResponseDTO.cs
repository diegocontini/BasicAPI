namespace BasicAPI.Features.Orders.DTOs;

public class VendaPostResponseDTO
{
    public VendaPostResponseDTO(int vendaCodigo, ICollection<int> itensCodigos)
    {
        this.codigo = vendaCodigo;
        this.itens = itensCodigos.Select(p => new VendaItemPostResponseDTO { Codigo = p }).ToList();
    }

    public int codigo { get; set; }
    public required IEnumerable<VendaItemPostResponseDTO> itens { get; set; }
}

public class VendaItemPostResponseDTO
{
    public int Codigo { get; set; }

}

