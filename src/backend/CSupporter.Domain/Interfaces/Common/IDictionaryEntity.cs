namespace CSupporter.Domain.Interfaces.Common;

public interface IDictionaryEntity<TCode> : IEntity
{
    public TCode Code { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime? ValidTo { get; set; }
}
