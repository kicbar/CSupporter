using CSupporter.Domain.Entities;
using CSupporter.Domain.Interfaces.Repositories;
using CSupporter.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSupporter.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly CsupporterDbContext _dbContext;

    public OrderRepository(CsupporterDbContext csupporterDbContext)
    {
        _dbContext = csupporterDbContext;
    }

    public async Task<IEnumerable<Order>> GetAllOrders(CancellationToken cancellationToken)
    {
        return await _dbContext.Orders
            .Include(x => x.Client)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Order>> GetAllOrdersForClient(int clientId, CancellationToken cancellationToken)
    {
        return await _dbContext.Orders
            .Where(x => x.ClientId == clientId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Order> CreateOrderForClient(Order order, CancellationToken cancellationToken)
    {
        await _dbContext.Orders.AddAsync(order, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return order;
    }
}
