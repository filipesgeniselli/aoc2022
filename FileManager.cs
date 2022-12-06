namespace AdventOfCode2022;

internal static class FileManager
{
    public static async Task<List<string>> ReadFromFileAsync(int day, bool test = true)
    {
        var path = $"{(test ? "InputTests" : "Inputs")}/Day{day:00}.txt";
        if (File.Exists(path))
        {
            var stringResult = await File.ReadAllLinesAsync(path);

            return stringResult.ToList();
        }

        return new List<string>();
    }
}
