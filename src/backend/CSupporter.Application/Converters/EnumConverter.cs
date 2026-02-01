using CSupporter.Application.Exceptions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CSupporter.Application.Converters;

public class EnumConverter<T> : JsonConverter<T> where T : struct, Enum
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();

        if (!string.IsNullOrEmpty(value))
            if (Enum.TryParse(value.ToString(), true, out T result))
                if (Enum.IsDefined(typeof(T), result.ToString()))
                    return result;

        throw new EnumValueNotAllowedException(value, typeof(T));
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
