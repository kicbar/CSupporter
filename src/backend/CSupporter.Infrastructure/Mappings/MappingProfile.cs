using AutoMapper;
using CSupporter.Application.CQRS.Clients.Command;
using CSupporter.Application.CQRS.Orders.Commands;
using CSupporter.Application.CQRS.Products.Commands;
using CSupporter.Application.CQRS.Users.Commands;
using CSupporter.Application.Models.DTOs;
using CSupporter.Domain.Entities;

namespace CSupporter.Infrastructure.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateClientCommand, Client>();

        CreateMap<CreateUserCommand, User>();

        CreateMap<CreateProductCommand, Product>();
        
        CreateMap<CreateOrderForClientCommand, Order>();

        CreateMap<Order, OrderDto>();

        CreateMap<Order, OrderForClientDto>();

        CreateMap<Order, OrderWithAuditDto>()
            .ReverseMap();

        CreateMap<Client, ClientDto>();

        CreateMap<Client, ClientWithAuditDto>();

        CreateMap<Client, ClientForOrderDto>();
    }
}
