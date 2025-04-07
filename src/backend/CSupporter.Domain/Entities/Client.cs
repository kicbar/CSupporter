using System.ComponentModel.DataAnnotations.Schema;

namespace CSupporter.Domain.Entities;

public class Client : BaseEntity
{
    [Column(Order = 2)]
    public string FirstName { get; set; }

    [Column(Order = 3)]
    public string LastName { get; set; }

    [Column(Order = 4)]
    public string ClientType { get; set; }
}
