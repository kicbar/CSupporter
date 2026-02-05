using AutoMapper;
using CSupporter.Application.Converters;
using CSupporter.Application.Exceptions;
using CSupporter.Application.Models.DTOs;
using CSupporter.Domain.Entities;
using CSupporter.Domain.Enums;
using CSupporter.Domain.Interfaces.Repositories;
using MediatR;
using System.Text.Json.Serialization;

namespace CSupporter.Application.CQRS.Orders.Commands;

public record CreateOrderForClientCommand : IRequest<OrderWithAuditDto>
{
    public int ClientId { get; set; }

    public string OrderNo { get; set; }

    public DateOnly OrderDate { get; set; }

    [JsonConverter(typeof(EnumConverter<ProducerType>))]
    public ProducerType? ProducerType { get; set; }

    public string AdditionalInfo { get; set; }
}

internal class CreateOrderForClientCommandHandler : IRequestHandler<CreateOrderForClientCommand, OrderWithAuditDto>
{
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRepository;
    private readonly IClientRepository _clientRepository;

    public CreateOrderForClientCommandHandler(IOrderRepository orderRepository, IClientRepository clientRepository, IMapper mapper)
    {
        _mapper = mapper;
        _orderRepository = orderRepository;
        _clientRepository = clientRepository;
    }

    public async Task<OrderWithAuditDto> Handle(CreateOrderForClientCommand request, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetClientById(request.ClientId, cancellationToken) 
            ?? throw new EntityNotFoundException(request.ClientId.ToString(), nameof(Client));

        var order = new Order()
        {
            ClientId = request.ClientId,
            OrderNo = request.OrderNo,
            OrderDate = request.OrderDate,
            ProducerType = (ProducerType)request.ProducerType,
            AdditionalInfo = request.AdditionalInfo,
        };

        var createdOrder = await _orderRepository.CreateOrderForClient(order, cancellationToken);

        return _mapper.Map<OrderWithAuditDto>(createdOrder);
    }
}
