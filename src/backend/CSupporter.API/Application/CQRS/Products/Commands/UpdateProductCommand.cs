using MediatR;
using CSupporter.API.Application.Services;
using CSupporter.API.Domain.Entities;
using CSupporter.API.Domain.Interfaces.IRepositories;
using CSupporter.API.Domain.Interfaces.IServices;
using System.Text.Json.Serialization;

namespace CSupporter.API.Application.CQRS.Products.Commands;

public record UpdateProductCommand : IRequest<Product>
{
    [JsonIgnore]
    public int ProductId { get; set; }
    public string ProductCode { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ProductType { get; set; }
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Product>
{
    private readonly IProductRepository _productRepository;
    private readonly ICurrentUserService _currentUserService;

    public UpdateProductCommandHandler(IProductRepository productRepository, ICurrentUserService currentUserService)
    {
        _productRepository = productRepository;
        _currentUserService = currentUserService;
    }

    public async Task<Product> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductById(command.ProductId, cancellationToken);
        product.ProductCode = command.ProductCode;
        product.Name = command.Name;
        product.Description = command.Description;
        product.ProductType = command.ProductType;
        var currentUser = _currentUserService?.UserEmail;
        if (currentUser is not null) 
            product.UpdateUser = currentUser;

        return await _productRepository.UpdateProduct(product, cancellationToken);
    }
}
