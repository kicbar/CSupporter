using AutoMapper;
using CSupporter.API.Application.CQRS.Clients.Command;
using CSupporter.API.Application.CQRS.Products.Commands;
using CSupporter.API.Application.CQRS.Users.Commands;
using CSupporter.API.Domain.Entities;

namespace CSupporter.API.Infrastructure.Mappings;

public class AnchorProfile : Profile
{
    public AnchorProfile()
    {
        CreateMap<CreateClientCommand, Client>();

        CreateMap<CreateUserCommand, User>();

        CreateMap<CreateProductCommand, Product>();
    }
}
