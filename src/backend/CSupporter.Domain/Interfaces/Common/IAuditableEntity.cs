namespace CSupporter.Domain.Interfaces.Common;

public interface IAuditableEntity : IEntity
{
    public DateTime UpdateDate { get; set; }

    public string UpdateUser { get; set; }
}
