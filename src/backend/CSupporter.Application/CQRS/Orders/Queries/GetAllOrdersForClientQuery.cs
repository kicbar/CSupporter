using AutoMapper;
using CSupporter.Application.Models.DTOs;
using CSupporter.Domain.Interfaces.Repositories;
using MediatR;

namespace CSupporter.Application.CQRS.Orders.Queries;

public record GetAllOrdersForClientQuery : IRequest<IEnumerable<OrderForClientDto>>
{
    public int ClientId { get; set; }
}

internal class GetAllOrdersForClient : IRequestHandler<GetAllOrdersForClientQuery, IEnumerable<OrderForClientDto>>
{
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRepository;

    public GetAllOrdersForClient(IMapper mapper, IOrderRepository orderRepository)
    {
        _mapper = mapper;
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<OrderForClientDto>> Handle(GetAllOrdersForClientQuery request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetAllOrdersForClient(request.ClientId, cancellationToken);

        return _mapper.Map<IEnumerable<OrderForClientDto>>(orders);
    }
}
