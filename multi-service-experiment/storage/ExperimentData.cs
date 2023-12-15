using Experiment;
using System.Text.Json.Nodes;
using System.Text.Json;

namespace Services.Storage;

internal struct ExperimentData
{
    public ExperimentData(Deck cards, bool hasCardMatch)
    {
        Cards = cards;
        HasCardMatch = hasCardMatch;
    }

    public Deck Cards
    {
        get; set;
    }

    public bool HasCardMatch
    {
        get; set;
    }

    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }

    public static ExperimentData? FromJson(string json)
    {
        return JsonSerializer.Deserialize<ExperimentData>(json);
    }
}
