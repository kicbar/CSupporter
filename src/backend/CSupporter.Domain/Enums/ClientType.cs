using System.ComponentModel;

namespace CSupporter.Domain.Enums;

public enum ClientType
{
    [Description("Klient indywidualny")]
    Individual,
    [Description("Klient biznesowy")]
    Business,
}
