using MediatR;
using RKAnchor.Server.Domain.Interfaces;

namespace RKAnchor.Server.Application.CQRS.Product.Commands;

public record CreateProductCommand : IRequest<Domain.Entities.Product>
{
    public string ProductCode { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ProductType { get; set; }
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Domain.Entities.Product>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Domain.Entities.Product> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Domain.Entities.Product()
        {
            ProductCode = command.ProductCode,
            Name = command.Name,
            Description = command.Description,
            ProductType = command.ProductType,
        };

        return await _productRepository.AddProduct(product, cancellationToken);
    }
}

