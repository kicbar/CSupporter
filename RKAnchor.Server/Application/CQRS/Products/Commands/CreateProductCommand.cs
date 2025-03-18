using AutoMapper;
using MediatR;
using RKAnchor.Server.Domain.Entities;
using RKAnchor.Server.Domain.Interfaces.IRepositories;

namespace RKAnchor.Server.Application.CQRS.Products.Commands;

public record CreateProductCommand : IRequest<Product>
{
    public string ProductCode { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ProductType { get; set; }
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;
    
    public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
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

