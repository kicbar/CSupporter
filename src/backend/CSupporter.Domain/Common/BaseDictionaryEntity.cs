using CSupporter.Domain.Interfaces.Common;

namespace CSupporter.Domain.Common;

public abstract class BaseDictionaryEntity<TCode> : IDictionaryEntity<TCode>
{
    public TCode Code { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime? ValidTo { get; set; }
    public DateTime InsertDate { get; set; }
    public string InsertUser { get; set; }

    public bool IsActive(DateTime at)
        => ValidFrom <= at && (ValidTo == null || at < ValidTo);
}
