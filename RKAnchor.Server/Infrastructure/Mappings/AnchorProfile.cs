using AutoMapper;
using RKAnchor.Server.Application.CQRS.Clients.Command;
using RKAnchor.Server.Application.CQRS.Products.Commands;
using RKAnchor.Server.Application.CQRS.Users.Commands;
using RKAnchor.Server.Domain.Entities;

namespace RKAnchor.Server.Infrastructure.Mappings;

public class AnchorProfile : Profile
{
    public AnchorProfile()
    {
        CreateMap<CreateClientCommand, Client>();

        CreateMap<CreateUserQuery, User>();

        CreateMap<CreateProductCommand, Product>();
    }
}
