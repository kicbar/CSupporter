using AutoMapper;
using CSupporter.Application.Converters;
using CSupporter.Domain.Entities;
using CSupporter.Domain.Enums;
using CSupporter.Domain.Interfaces.Repositories;
using MediatR;
using System.Text.Json.Serialization;

namespace CSupporter.Application.CQRS.Products.Commands;

public record CreateProductCommand : IRequest<Product>
{
    public string Name { get; set; }
    public string Description { get; set; }

    [JsonConverter(typeof(EnumConverter<ProductType>))]
    public ProductType? ProductType { get; set; }
    public string ProductCode { get; set; }
}

internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<Product> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(command);

        return await _productRepository.AddProduct(product, cancellationToken);
    }
}

