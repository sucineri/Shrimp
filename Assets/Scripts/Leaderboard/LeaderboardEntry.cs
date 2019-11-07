using System.Collections.Generic;
using Newtonsoft.Json;

public class LeaderboardEntry
{
    [JsonProperty("score")]
    public float Score;

    [JsonProperty("timestamp")]
    public long TimeStamp;

    [JsonProperty("name")]
    public string Name;

    public LeaderboardEntry(float score, long timeStamp, string name)
    {
        Score = score;
        TimeStamp = timeStamp;
        Name = name;
    }

    public Dictionary<string, object> ToDictionary()
    {
        return new Dictionary<string, object>
        {
            ["score"] = Score,
            ["timestamp"] = TimeStamp,
            ["name"] = Name
        };
    }
}

