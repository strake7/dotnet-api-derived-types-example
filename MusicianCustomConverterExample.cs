using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// You could write a custom JsonConverter to tell the serializer how to act.
public class MusicianConverter : JsonConverter<Musician>
{
    public override Musician ReadJson(JsonReader reader, Type objectType, [AllowNull] Musician existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var jObject = JObject.Load(reader);
        var typeDiscriminator = jObject["instrumentType"].Value<string>();
        switch (typeDiscriminator)
        {
            case "Guitar":
                return serializer.Deserialize<Musician.Guitarist>(reader);              
            case "Piano":
                return serializer.Deserialize<Musician.Pianist>(reader);    
            default:
                throw new NotSupportedException();
        }   
    }

    public override void WriteJson(JsonWriter writer, [AllowNull] Musician value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}