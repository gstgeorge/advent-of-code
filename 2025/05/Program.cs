string[] data = File.ReadAllLines("data.txt");

int idx = data.IndexOf("");

List<(long Start, long End)> ranges = [];

int partA = 0;
long partB = 0;

foreach (var record in data[..idx])
{
    var range = record.Split("-");

    ranges.Add((long.Parse(range[0]), long.Parse(range[1])));
}

foreach (var record in data[(idx+1)..].Select(x => long.Parse(x)))
{
    if (ranges.Any(x => x.Start <= record && x.End >= record))
    {
        partA++;
    }
}

bool consolidated = false;

while (!consolidated)
{
    consolidated = true;

    ranges.Sort((x, y) => x.Start.CompareTo(y.Start));

    for (int i = ranges.Count - 1; i > 0; i--)
    {
        if (ranges[i].Start <= ranges[i-1].End)
        {
            if (ranges[i].End <= ranges[i-1].End)
            {
                ranges.RemoveAt(i);
            }

            else
            {
                ranges[i] = (ranges[i - 1].End + 1, ranges[i].End);
                consolidated = false;
            }
        }
    }
}

partB = ranges.Select(x => x.End - x.Start + 1).Sum();

Console.WriteLine($"Part A: {partA}");
Console.WriteLine($"Part B: {partB}");
