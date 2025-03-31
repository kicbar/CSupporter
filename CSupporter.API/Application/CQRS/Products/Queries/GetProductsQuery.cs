using MediatR;
using RKAnchor.Server.Domain.Interfaces.IRepositories;

namespace RKAnchor.Server.Application.CQRS.Products.Queries;

public record GetProductsQuery : IRequest<IEnumerable<Domain.Entities.Product>> { }

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<Domain.Entities.Product>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<Domain.Entities.Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return await _productRepository.GetAllProducts(cancellationToken);
    }
}