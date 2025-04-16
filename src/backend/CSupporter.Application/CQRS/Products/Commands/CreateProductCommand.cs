using AutoMapper;
using CSupporter.Application.IServices;
using CSupporter.Domain.Entities;
using CSupporter.Domain.Enums;
using CSupporter.Domain.Interfaces.Repositories;
using MediatR;

namespace CSupporter.Application.CQRS.Products.Commands;

public record CreateProductCommand : IRequest<Product>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ProductType ProductType { get; set; }
    public string ProductCode { get; set; }
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;
    private readonly ICurrentUserService _currentUserService;

    public CreateProductCommandHandler(IMapper mapper, IProductRepository productRepository, ICurrentUserService currentUserService)
    {
        _mapper = mapper;
        _productRepository = productRepository;
        _currentUserService = currentUserService;
    }

    public async Task<Product> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var currentUser = _currentUserService?.UserEmail;
        var product = _mapper.Map<Product>(command);
        if (currentUser is not null)
        {
            product.InsertUser = currentUser;
            product.UpdateUser = currentUser;
        }

        return await _productRepository.AddProduct(product, cancellationToken);
    }
}

