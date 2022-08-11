using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NoarExtensions;

public class EpochDateTimeConverter : DateTimeConverterBase
{
  public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
  {
    string str = String.Empty;

    if (!(value is DateTime dateTimeData))
    {
      throw new Exception($"{GetType()} :: Error while converting DateTime to Epoch. Type was unexpected.");
    }
    
    str = dateTimeData.ToEpoch().ToString(CultureInfo.InvariantCulture);
    
    writer.WriteValue(str);
  }
  
  public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
  {
    if (reader.TokenType != JsonToken.String)
    {
      throw new JsonSerializationException($"{GetType()} :: Error while deserializing DateTime to Epoch. Unexpected type.");
    }

    string str = reader.Value?.ToString();

    if (String.IsNullOrEmpty(str))
    {
      return null;
    }

    return double.Parse(str).ToDateTime().ToLocalTime();
  }
}
