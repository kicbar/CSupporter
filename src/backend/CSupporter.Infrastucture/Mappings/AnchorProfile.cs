using AutoMapper;
using CSupporter.Application.CQRS.Clients.Command;
using CSupporter.Application.CQRS.Products.Commands;
using CSupporter.Application.CQRS.Users.Commands;
using CSupporter.Domain.Entities;

namespace CSupporter.Infrastructure.Mappings;

public class AnchorProfile : Profile
{
    public AnchorProfile()
    {
        CreateMap<CreateClientCommand, Client>();

        CreateMap<CreateUserCommand, User>();

        CreateMap<CreateProductCommand, Product>();
    }
}
