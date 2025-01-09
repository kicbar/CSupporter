namespace RKAnchor.Server.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string ProductCode { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ProductType { get; set; }
    public string InsertUser { get; set; }
    public DateTime InsertDate { get; set; }
    public string UpdateUser { get; set; }
    public DateTime UpdateDate { get; set; }
}
