using AutoMapper;
using BasicAPI.Features.Orders.DTOs;
using BasicAPI.Models.Entities;

namespace BasicAPI.Config;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<VendaPostDTO, Venda>();
        CreateMap<VendaItemPostDTO, VendaItem>();
        CreateMap<VendaGetDTO, Venda>();
    }
}