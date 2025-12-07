using System.Text.RegularExpressions;

string[] data = File.ReadAllLines("data.txt")[0].Split(",");

Regex reA = new(@"^((?:[1-9]){1}(?:\d)*)\1$");
Regex reB = new(@"^((?:[1-9]){1}(?:\d)*)\1+$");

long totalA = 0;
long totalB = 0;

foreach (var record in data)
{
    var range = record.Split("-");

    for (long i = long.Parse(range[0]); i <= long.Parse(range[1]); i++)
    {
        if (reA.IsMatch(i.ToString()))
        {
            totalA += i;
        }

        if (reB.IsMatch(i.ToString()))
        {
            totalB += i;
        }
    }
}

Console.WriteLine($"Part A: {totalA}");
Console.WriteLine($"Part B: {totalB}");
