using CSupporter.Application.Converters;
using CSupporter.Application.Exceptions;
using CSupporter.Domain.Entities;
using CSupporter.Domain.Enums;
using CSupporter.Domain.Interfaces.Repositories;
using MediatR;
using System.Text.Json.Serialization;

namespace CSupporter.Application.CQRS.Orders.Commands;

public record CreateOrderForClientCommand : IRequest<Order>
{
    public int ClientId { get; set; }

    public string OrderNo { get; set; }

    public DateOnly OrderDate { get; set; }

    [JsonConverter(typeof(EnumConverter<ProducerType>))]
    public ProducerType? ProducerType { get; set; }

    public string AdditionalInfo { get; set; }
}

internal class CreateOrderForClientCommandHandler : IRequestHandler<CreateOrderForClientCommand, Order>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IClientRepository _clientRepository;

    public CreateOrderForClientCommandHandler(IOrderRepository orderRepository, IClientRepository clientRepository)
    {
        _orderRepository = orderRepository;
        _clientRepository = clientRepository;
    }

    public async Task<Order> Handle(CreateOrderForClientCommand request, CancellationToken cancellationToken)
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
        //return as dto
        return await _orderRepository.CreateOrderForClient(order, cancellationToken);
    }
}
