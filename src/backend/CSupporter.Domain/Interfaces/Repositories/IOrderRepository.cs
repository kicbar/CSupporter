using CSupporter.Domain.Entities;

namespace CSupporter.Domain.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllOrders(CancellationToken cancellationToken);

    Task<IEnumerable<Order>> GetAllOrdersForClient(int clientId, CancellationToken cancellationToken);
    
    Task<Order> CreateOrderForClient(Order order, CancellationToken cancellationToken);
}