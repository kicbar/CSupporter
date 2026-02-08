using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CSupporter.Application.Converters;

public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    private const string WriteFormat = "yyyy-MM-dd";

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var value = reader.GetString();

            if (string.IsNullOrWhiteSpace(value))
                return default;

            if (DateOnly.TryParseExact(
                    value,
                    WriteFormat,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var dateOnly))
            {
                return dateOnly;
            }

            if (DateTime.TryParse(
                    value,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.RoundtripKind,
                    out var dateTime))
            {
                return DateOnly.FromDateTime(dateTime);
            }

            throw new JsonException($"Invalid DateOnly value: {value}");
        }

        if (reader.TokenType == JsonTokenType.Number &&
            reader.TryGetDateTime(out var dt))
        {
            return DateOnly.FromDateTime(dt);
        }

        throw new JsonException(
            $"Unexpected token parsing DateOnly. Token: {reader.TokenType}");
    }

    public override void Write(Utf8JsonWriter writer,
        DateOnly value,
        JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString(WriteFormat));
}
