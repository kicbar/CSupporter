using MediatR;
using RKAnchor.Server.Domain.Interfaces.IRepositories;

namespace RKAnchor.Server.Application.CQRS.Products.Commands;

public record RemoveProductCommand : IRequest<bool>
{
    public int ProductId { get; set; }
}

public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommand, bool>
{
    private IProductRepository _productRepository;

    public RemoveProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<bool> Handle(RemoveProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductById(command.ProductId, cancellationToken);

        return await _productRepository.RemoveProduct(product, cancellationToken);
    }
}
