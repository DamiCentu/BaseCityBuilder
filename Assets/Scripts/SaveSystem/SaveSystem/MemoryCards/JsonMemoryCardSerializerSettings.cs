using Newtonsoft.Json;

public class JsonMemoryCardSerializerSettings : JsonSerializerSettings
{
    public JsonMemoryCardSerializerSettings()
    {
        Formatting = Formatting.Indented;
        TypeNameHandling = TypeNameHandling.Auto;
        NullValueHandling = NullValueHandling.Ignore;
        Converters.Add(new EpochDateTimeConverter());
        ContractResolver = new JsonMemoryCardContractResolver(global::JsonMemoryCardContractResolver.Serialize.Fields);
    }
    
}