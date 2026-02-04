using CSupporter.Domain.Common;
using CSupporter.Domain.Enums;

namespace CSupporter.Domain.Entities;

public class OrderItem : BaseAuditableEntity<int>
{
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public ProductType ProductType { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public string Colour { get; set; }

    public string AdditionalInfo { get; set; }
}
