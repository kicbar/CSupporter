using CSupporter.Application.Converters;
using CSupporter.Application.Interfaces;
using CSupporter.Domain.Entities;
using CSupporter.Domain.Enums;
using CSupporter.Domain.Interfaces.Repositories;
using MediatR;
using System.Text.Json.Serialization;

namespace CSupporter.Application.CQRS.Products.Commands;

public record UpdateProductCommand : IRequest<Product>
{
    [JsonIgnore]
    public int ProductId { get; set; }
    public string ProductCode { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    [JsonConverter(typeof(EnumConverter<ProductType>))]
    public ProductType? ProductType { get; set; }
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Product>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductById(command.ProductId, cancellationToken);
        product.ProductCode = command.ProductCode;
        product.Name = command.Name;
        product.Description = command.Description;
        if(command.ProductType is not null) 
        product.ProductType = (ProductType)command.ProductType;

        return await _productRepository.UpdateProduct(product, cancellationToken);
    }
}
