using MediatR;
using RKAnchor.Server.Domain.Entities;
using RKAnchor.Server.Domain.Interfaces;

namespace RKAnchor.Server.Application.CQRS.Products.Commands;

public record CreateProductCommand : IRequest<Product>
{
    public string ProductCode { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ProductType { get; set; }
    public Product MyProperty { get; set; }
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Domain.Entities.Product> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Product()
        {
            ProductCode = command.ProductCode,
            Name = command.Name,
            Description = command.Description,
            ProductType = command.ProductType,
        };

        return await _productRepository.AddProduct(product, cancellationToken);
    }
}

