using CSupporter.Domain.Common;
using CSupporter.Domain.Enums;

namespace CSupporter.Domain.Entities;

public class Client : BaseAuditableEntity<int>
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public ClientType ClientType { get; set; }
}
