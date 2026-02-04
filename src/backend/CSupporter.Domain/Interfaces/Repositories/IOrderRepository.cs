using CSupporter.Domain.Entities;

namespace CSupporter.Domain.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrderForClient(Order order, CancellationToken cancellationToken);
    }
}