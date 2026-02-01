using CSupporter.Domain.Interfaces.Common;

namespace CSupporter.Domain.Common;

public abstract class BaseAuditableEntity<TId> : BaseEntity<TId>, IAuditableEntity
{
    public DateTime UpdateDate { get; set; }
    public string UpdateUser { get; set; }
    public DateTime InsertDate { get; set; }
    public string InsertUser { get; set; }
}
