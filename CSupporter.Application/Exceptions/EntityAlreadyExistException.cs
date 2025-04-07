namespace CSupporter.Application.Exceptions;

public class EntityAlreadyExistException  : Exception
{
    public EntityAlreadyExistException()
    {
    }

    public EntityAlreadyExistException(string entityId, string entityName)
    : base($"Entity [{entityName}] with id: [{entityId}] already exist!")
    {
    }

    public EntityAlreadyExistException(string entityId, string entityName, Exception exc)
        : base($"Entity {entityName} with id: {entityId} already exist! ErrorMsg: {exc.Message}")
    {
    }
}
