namespace CSupporter.Application.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException()
    {
    }

    public EntityNotFoundException(string entityId, string entityName)
    : base($"Entity [{entityName}] with id: [{entityId}] not exist!")
    {
    }

    public EntityNotFoundException(string entityId, string entityName, Exception exc)
        : base($"Entity {entityName} with id: {entityId} not exist! ErrorMsg: {exc.Message}")
    {
    }

}
