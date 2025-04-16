using CSupporter.Domain.Interfaces.Services;

namespace CSupporter.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime CurrentDateTime => DateTime.UtcNow;
}
