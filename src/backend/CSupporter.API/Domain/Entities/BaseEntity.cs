using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RKAnchor.Server.Domain.Entities;

public abstract class BaseEntity
{
    [Key]
    [Column(Order = 1)]
    public int Id { get; set; }

    [Column(Order = 100)]
    public string InsertUser { get; set; }

    [Column(Order = 101)]
    public DateTime InsertDate { get; set; }
   
    [Column(Order = 102)]
    public string UpdateUser { get; set; }
    
    [Column(Order = 103)]
    public DateTime UpdateDate { get; set; }
}
