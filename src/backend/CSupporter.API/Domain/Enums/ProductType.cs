using System.ComponentModel;

namespace CSupporter.API.Domain.Enums;

public enum ProductType
{
    [Description("Okno")]
    Window,

    [Description("Drzwi")]
    Door,

    [Description("Brama garażowa")]
    Gate,

    [Description("Roleta zewnętrzna")]
    Roller,
}
