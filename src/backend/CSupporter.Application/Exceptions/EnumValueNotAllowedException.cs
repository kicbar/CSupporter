namespace CSupporter.Application.Exceptions;

public class EnumValueNotAllowedException : Exception
{
    public EnumValueNotAllowedException()
    {
    }

    public EnumValueNotAllowedException(string? enumValue, object type)
    : base($"Not allowed value {enumValue} for type: {type}")
    {
    }

    public EnumValueNotAllowedException(string? enumValue, object type, Exception exc)
        : base($"Not allowed value {enumValue} for type: {type}, ErrorMsg: {exc.Message}")
    {
    }
}
