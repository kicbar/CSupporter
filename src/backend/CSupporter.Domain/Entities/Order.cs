using CSupporter.Domain.Common;
using CSupporter.Domain.Enums;

namespace CSupporter.Domain.Entities;

public class Order : BaseAuditableEntity<int>
{
    public string OrderNo { get; set; }

    public DateOnly OrderDate { get; set; }

    public ProducerType ProducerType { get; set; }

    public string AdditionalInfo { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public List<OrderItem> OrderItems { get; set; }
}
