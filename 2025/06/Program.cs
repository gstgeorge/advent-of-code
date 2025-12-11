using System.Text;
using System.Text.RegularExpressions;

string[] file = File.ReadAllLines("data.txt");

long partA = 0;
long partB = 0;

Regex reOps = new(@"(\+|\*)");
char[] ops = [.. reOps.Matches(file.Last()).Select(x => Convert.ToChar(x.Value))];

List<(int Start, int End)> indexes = [];

int startIdx = 0;

for (int i = 0; i < file[0].Length; i++)
{
    if (file.All(x => x[i] == ' '))
    {
        indexes.Add((startIdx, i-1));

        startIdx = i+1;
    }
}

indexes.Add((startIdx, file[0].Length - 1));

List<int> values = [];
StringBuilder sb = new();

for (int idx = 0; idx < indexes.Count; idx++)
{
    // Part A
    for (int i = 0; i < file.Length - 1; i++)
    {
        for (int j = indexes[idx].Start; j <= indexes[idx].End; j++)
        {
            sb.Append(file[i][j]);
        }
        
        if (!string.IsNullOrWhiteSpace(sb.ToString()))
        {
            values.Add(int.Parse(sb.ToString()));

            sb.Clear();
        }
    }

    partA += CalcAnswer(values, ops[idx]);

    values.Clear();
    sb.Clear();

    // Part B
    for (int i = indexes[idx].End; i >= indexes[idx].Start; i--)
    {
        for (int j = 0; j < file.Length - 1; j++)
        {
            sb.Append(file[j][i]);
        }

        if (!string.IsNullOrWhiteSpace(sb.ToString()))
        {
            values.Add(int.Parse(sb.ToString()));

            sb.Clear();
        }
    }

    partB += CalcAnswer(values, ops[idx]);

    values.Clear();
    sb.Clear();
}

Console.WriteLine($"Part A: {partA}");
Console.WriteLine($"Part B: {partB}");

long CalcAnswer(List<int> values, char op)
{
    long answer = values[0];

    foreach (int value in values[1..])
    {
        switch(op)
        {
            case '+':
                answer += value;
                break;
            case '*':
                answer *= value;
                break;
        }
    }

    return answer;
}
