using System;
using Newtonsoft.Json;
using SixLabors.Primitives;

namespace ContentFactory.Features.TexturePacker
{
    public class SizeJsonConverter : JsonConverter<Size>
    {
        public override void WriteJson(JsonWriter writer, Size size, JsonSerializer serializer)
        {
            var previousFormatting = serializer.Formatting;
            writer.Formatting = Formatting.None;
            writer.WriteStartObject();
            writer.WritePropertyName("width");
            writer.WriteValue(size.Width);
            writer.WritePropertyName("height");
            writer.WriteValue(size.Height);
            writer.WriteEndObject();
            writer.Formatting = previousFormatting;
        }

        public override Size ReadJson(JsonReader reader, Type objectType, Size existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize<Size>(reader);
        }
    }
}