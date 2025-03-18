using MediatR;
using RKAnchor.Server.Domain.Interfaces.IRepositories;
using System.Text.Json.Serialization;

namespace RKAnchor.Server.Application.CQRS.Products.Commands;

public record UpdateProductCommand : IRequest<Domain.Entities.Product>
{
    [JsonIgnore]
    public int ProductId { get; set; }
    public string ProductCode { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ProductType { get; set; }
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Domain.Entities.Product>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Domain.Entities.Product> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductById(command.ProductId, cancellationToken);
        product.ProductCode = command.ProductCode;
        product.Name = command.Name;
        product.Description = command.Description;
        product.ProductType = command.ProductType;

        return await _productRepository.UpdateProduct(product, cancellationToken);
    }
}
