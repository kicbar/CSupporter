using System.ComponentModel.DataAnnotations.Schema;

namespace RKAnchor.Server.Domain.Entities;

public class Product : BaseEntity
{
    [Column(Order = 2)]
    public string ProductCode { get; set; }

    [Column(Order = 3)]
    public string Name { get; set; }

    [Column(Order = 4)]
    public string Description { get; set; }

    [Column(Order = 5)]
    public string ProductType { get; set; }
}
