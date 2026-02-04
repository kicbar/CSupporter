using CSupporter.Domain.Common;
using CSupporter.Domain.Enums;
using System.Text.Json.Serialization;

namespace CSupporter.Domain.Entities;

public class Client : BaseAuditableEntity<int>
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }

    public string Email { get; set; }

    public ClientType ClientType { get; set; }

    [JsonIgnore]
    public List<Order> Orders { get; set; }
}
