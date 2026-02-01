using CSupporter.Domain.Common;
using CSupporter.Domain.Enums;

namespace CSupporter.Domain.Entities;

public class Product : BaseAuditableEntity<int>
{
    public string ProductCode { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public ProductType ProductType { get; set; }
}
