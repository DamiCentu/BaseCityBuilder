using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

public class JsonMemoryCardContractResolver : DefaultContractResolver
{
    [Flags]
    public enum Serialize
    {
        Fields = 1,
        Properties = 2,
        All = Fields | Properties
    }

    public Serialize ToSerialize { get; set; } = Serialize.Fields;

    public BindingFlags BindingFlags { get; set; } =
        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

    public JsonMemoryCardContractResolver(Serialize toSerialize = Serialize.All,
        BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
    {
        BindingFlags = flags;
        ToSerialize = toSerialize;
    }
    
    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
    {
        List<MemberInfo> members = new List<MemberInfo>();

        if (ToSerialize.HasFlag(Serialize.Fields))
        {
            members.AddRange(type.GetFields(BindingFlags));
        }
        
        if (ToSerialize.HasFlag(Serialize.Properties))
        {
            members.AddRange(type.GetProperties(BindingFlags));
        }
        
        List<JsonProperty> properties = new List<JsonProperty>();

        foreach (MemberInfo member in members)
        {
            properties.Add(GetProperty(member, memberSerialization));
        }
        
        return properties;
    }

    private JsonProperty GetProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        JsonProperty property = CreateProperty(member, memberSerialization);
        property.Writable = true;
        property.Readable = true;
        return property;
    }
}