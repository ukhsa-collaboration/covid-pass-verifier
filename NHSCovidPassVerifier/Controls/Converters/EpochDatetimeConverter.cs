using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NHSCovidPassVerifier.Controls.Converters
{
    public class EpochDatetimeConverter: DateTimeConverterBase
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);


        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null) { return null; }
            return Epoch.AddMilliseconds((long)reader.Value / 1000d);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue(((DateTime)value - Epoch).TotalMilliseconds + "000");
        }
    }
}