using MediatR;
using CSupporter.API.Domain.Interfaces.IRepositories;

namespace CSupporter.API.Application.CQRS.Products.Queries;

public record GetProductQuery : IRequest<Domain.Entities.Product>
{
    public int ProductId { get; set; }
}

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Domain.Entities.Product>
{
    private readonly IProductRepository _productRepository;

    public GetProductQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Domain.Entities.Product> Handle(GetProductQuery command, CancellationToken cancellationToken)
    {
        return await _productRepository.GetProductById(command.ProductId, cancellationToken);
    }
}
