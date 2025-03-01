using System.ComponentModel;

namespace RKAnchor.Server.Domain.Enums;

public enum ClientType
{
    [Description("Klient indywidualny")]
    Individual,
    [Description("Klient biznesowy")]
    Business,
}
