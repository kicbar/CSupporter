using CSupporter.Domain.Interfaces.Services;

namespace CSupporter.Infrastucture.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime CurrentDateTime => DateTime.UtcNow;
}
