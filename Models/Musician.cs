using JsonSubTypes;
using Newtonsoft.Json;

// one possible solution is to use JsonSubTypes attributes
// [JsonConverter(typeof(JsonSubtypes), nameof(Musician.InstrumentType))]
// [JsonSubtypes.KnownSubType(typeof(Guitarist), "Guitar")]
// [JsonSubtypes.KnownSubType(typeof(Pianist), "Piano")]
public abstract class Musician
{
    [JsonRequired]
    public abstract string InstrumentType { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public class Guitarist : Musician
    {        
        public override string InstrumentType { get; } = "Guitar";
        public int StringCount { get; set; }
        public bool CanPlayWonderwall { get; set; }
    }

    public class Pianist : Musician
    {
        public override string InstrumentType { get; } = "Piano";
        public int HandSpanRating { get; set; }
    }
}

