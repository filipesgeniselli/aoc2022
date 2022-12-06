namespace AdventOfCode2022.Days;

internal class Day02 : IDay
{
    private const string Rock = "R";

    private const string Paper = "P";

    private const string Scissors = "S";


    public void Part1(List<string> inputs)
    {
        var result = inputs
            .Select(x => (opp: TranslateGame(x[0]), elf: TranslateGame(x[2])))
            .Select(x => GamePoints(x.opp, x.elf) + TypePoints(x.elf));

        Console.WriteLine(result.Sum());
    }

    public void Part2(List<string> inputs)
    {
        var result = inputs
            .Select(x => (opp: TranslateGame(x[0]), elf: GetConditionalHand(x[2], TranslateGame(x[0]))))
            .Select(x => GamePoints(x.opp, x.elf) + TypePoints(x.elf));

        Console.WriteLine(result.Sum());
    }
    private static string GetConditionalHand(char condition, string opponent) => (condition, opponent) switch
    {
        ('X', string opp) => opp switch { Rock => Scissors, Paper => Rock, Scissors => Paper, _ => string.Empty }, //Loose
        ('Y', string opp) => opp switch { Rock => Rock, Paper => Paper, Scissors => Scissors, _ => string.Empty }, //Draw
        ('Z', string opp) => opp switch { Rock => Paper, Paper => Scissors, Scissors => Rock, _ => string.Empty }, //Win
        _ => string.Empty
    };

    private static string TranslateGame(char s)
    {
        return s switch
        {
            'A' => Rock,
            'X' => Rock,
            'B' => Paper,
            'Y' => Paper,
            'C' => Scissors,
            'Z' => Scissors,
            _ => "X"
        };
    }

    private static int GamePoints(string first, string second) => (first, second) switch 
    {
        (Rock, string s) => s switch { Rock => 3, Paper => 6, Scissors => 0, _ => 0 },
        (Paper, string s) => s switch { Rock => 0, Paper => 3, Scissors => 6, _ => 0 },
        (Scissors, string s) => s switch { Rock => 6, Paper => 0, Scissors => 3, _ => 0 },
        _ => 0
    };

    private static int TypePoints(string type) => type switch
    {
        Rock => 1,
        Paper => 2,
        Scissors => 3,
        _ => 0
    };
}
