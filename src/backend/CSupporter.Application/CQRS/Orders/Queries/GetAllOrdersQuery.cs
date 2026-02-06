using AutoMapper;
using CSupporter.Application.Models.DTOs;
using CSupporter.Domain.Interfaces.Repositories;
using MediatR;

namespace CSupporter.Application.CQRS.Orders.Queries;

public record GetAllOrdersQuery : IRequest<IEnumerable<OrderWithAuditDto>> { }

internal class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderWithAuditDto>>
{
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRepository;

    public GetAllOrdersQueryHandler(IMapper mapper, IOrderRepository orderRepository)
    {
        _mapper = mapper;
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<OrderWithAuditDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetAllOrders(cancellationToken);

        return _mapper.Map<IEnumerable<OrderWithAuditDto>>(orders);
    }
}
