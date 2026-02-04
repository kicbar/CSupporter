using CSupporter.Domain.Entities;
using CSupporter.Domain.Interfaces.Repositories;
using CSupporter.Infrastructure.Data;

namespace CSupporter.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly CsupporterDbContext _dbContext;

    public OrderRepository(CsupporterDbContext csupporterDbContext)
    {
        _dbContext = csupporterDbContext;
    }

    public async Task<Order> CreateOrderForClient(Order order, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(order, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return order;
    }
}
