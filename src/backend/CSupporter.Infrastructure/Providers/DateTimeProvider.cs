using CSupporter.Application.Interfaces;

namespace CSupporter.Infrastructure.Providers;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime CurrentDateTime => DateTime.UtcNow;
}
