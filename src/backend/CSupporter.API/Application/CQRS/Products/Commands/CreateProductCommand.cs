﻿using AutoMapper;
using Azure.Core;
using MediatR;
using CSupporter.API.Application.Services;
using CSupporter.API.Domain.Entities;
using CSupporter.API.Domain.Interfaces.IRepositories;
using CSupporter.API.Domain.Interfaces.IServices;

namespace CSupporter.API.Application.CQRS.Products.Commands;

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

