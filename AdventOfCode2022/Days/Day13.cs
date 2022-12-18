using AdventOfCode2022.Extensions;
using System.Text.Json;

namespace AdventOfCode2022.Days;

public class Day13 : IDay
{
    public string Part1(List<string> inputs)
    {
        var pairs = inputs.Split(x => string.IsNullOrEmpty(x)).ToList();

        var result = pairs.Select((x, index) => (result: ComparerUtils.Compare(x.First(), x.Last()), index: index + 1))
            .Where(x => x.result < 0).Sum(x => x.index);

        return result.ToString();
    }

    public string Part2(List<string> inputs)
    {
        inputs.Add("[[2]]");
        inputs.Add("[[6]]");

        var result = inputs.Where(x => !string.IsNullOrEmpty(x))
            .OrderBy(x => x, new DistressSignalComparer())
            .ToList();

        return result.Select((x, index) => (signal: x, index: index + 1))
            .Where(x => x.signal == "[[2]]" || x.signal == "[[6]]")
            .Select(x => x.index)
            .Aggregate((a, b) => a * b)
            .ToString();
    }
}

static class ComparerUtils
{
    public static int Compare(string s1, string s2) =>
        Compare(JsonSerializer.Deserialize<JsonElement>(s1), JsonSerializer.Deserialize<JsonElement>(s2));

    public static int Compare(JsonElement j1, JsonElement j2) =>
        (j1.ValueKind, j2.ValueKind) switch
        {
            (JsonValueKind.Number, JsonValueKind.Number) =>
                j1.GetInt32() - j2.GetInt32(),
            (JsonValueKind.Number, _) =>
                DoCompare(JsonSerializer.Deserialize<JsonElement>($"[{j1.GetInt32()}]"), j2),
            (_, JsonValueKind.Number) =>
                DoCompare(j1, JsonSerializer.Deserialize<JsonElement>($"[{j2.GetInt32()}]")),
            _ => DoCompare(j1, j2),
        };

    public static int DoCompare(JsonElement j1, JsonElement j2)
    {
        int res;
        JsonElement.ArrayEnumerator e1 = j1.EnumerateArray();
        JsonElement.ArrayEnumerator e2 = j2.EnumerateArray();
        while (e1.MoveNext() && e2.MoveNext())
            if ((res = Compare(e1.Current, e2.Current)) != 0)
                return res;
        return j1.GetArrayLength() - j2.GetArrayLength();
    }
}

class DistressSignalComparer : IComparer<string>
{
    public int Compare(string? x, string? y)
    {
        return ComparerUtils.Compare(x, y);
    }
}
