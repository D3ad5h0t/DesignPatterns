using System.Text;
using System.Xml.Serialization;

var stats = new CountryStats();
stats.Capitals.Add("France", "Paris");

var xs = new XmlSerializer(typeof(CountryStats));
var sb = new StringBuilder();
var sw = new StringWriter(sb);
xs.Serialize(sw, stats);

var newStats = (CountryStats)xs.Deserialize(new StringReader(sb.ToString()));
Console.WriteLine(newStats.Capitals["France"]);


public class CountryStats
{
    [XmlIgnore]
    public Dictionary<string, string> Capitals { get; set; } = new Dictionary<string, string>();

    public (string, string)[] CapitalsSerializable
    {
        get => Capitals.Keys.Select(country => (country, Capitals[country])).ToArray();
        set => Capitals = value.ToDictionary(x => x.Item1, x => x.Item2);
    }
}