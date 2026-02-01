namespace CSupporter.Domain.Interfaces.Common;

public interface IEntity
{
    public DateTime InsertDate { get; set; }

    public string InsertUser { get; set; }
}
