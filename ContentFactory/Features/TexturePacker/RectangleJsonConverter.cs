using System;
using Newtonsoft.Json;
using SixLabors.Primitives;

namespace ContentFactory.Features.TexturePacker
{
    public class RectangleJsonConverter : JsonConverter<Rectangle>
    {
        public override void WriteJson(JsonWriter writer, Rectangle rectangle, JsonSerializer serializer)
        {
            var previousFormatting = serializer.Formatting;
            writer.Formatting = Formatting.None;
            writer.WriteStartObject();
            writer.WritePropertyName("x");
            writer.WriteValue(rectangle.X);
            writer.WritePropertyName("y");
            writer.WriteValue(rectangle.Y);
            writer.WritePropertyName("width");
            writer.WriteValue(rectangle.Width);
            writer.WritePropertyName("height");
            writer.WriteValue(rectangle.Height);
            writer.WriteEndObject();
            writer.Formatting = previousFormatting;
        }

        public override Rectangle ReadJson(JsonReader reader, Type objectType, Rectangle existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize<Rectangle>(reader);
        }
    }
}