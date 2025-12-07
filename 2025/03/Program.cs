string[] data = File.ReadAllLines("data.txt");

long totalA = 0;
long totalB = 0;

foreach (var line in data)
{
    List<int> numbers = [.. line.ToCharArray().Select(x => int.Parse(x.ToString()))];

    totalA += CalcTotal(numbers, 2);
    totalB += CalcTotal(numbers, 12);
}

Console.WriteLine($"Part A: {totalA}");
Console.WriteLine($"Part B: {totalB}");

static long CalcTotal(List<int> numbers, int size)
{
    int idx = numbers.Count - size;

    List<int> val = numbers[idx..];

    for (int i = idx - 1; i >= 0; i--)
    {
        if (numbers[i] >= val[0])
        {
            val.Insert(0, numbers[i]);

            for (int j = 1; j < val.Count - 1; j++)
            {
                if (val[j] < val[j + 1])
                {
                    val.RemoveAt(j);
                    break;
                }

                if (j + 1 == val.Count - 1)
                {
                    val.RemoveAt(val[j] < val[j + 1] ? j : j + 1);
                    break;
                }
            }
        }
    }

    return long.Parse(string.Join("", val.Select(x => x.ToString())));
}
