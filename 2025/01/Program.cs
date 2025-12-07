using System.Text.RegularExpressions;

string[] file = File.ReadAllLines("data.txt");

Regex re = new(@"^(L|R)(\d+)$");

(string, int)[] data = [.. file.Select(x => re.Match(x)).Select(x => (x.Groups[1].ToString(), int.Parse(x.Groups[2].ToString())))];

int dialStart = 50;
int dialEnd = 0;

int passwordA = 0;
int passwordB = 0;

foreach (var (direction, distance) in data)
{
    dialEnd = direction switch
    {
        "L" => (dialStart - distance) % 100,
        "R" => (dialStart + distance) % 100,
        _   => throw new InvalidDataException()
    };

    if (dialEnd < 0) 
    {
        dialEnd += 100; 
    }

    if (dialEnd == 0) 
    {
        passwordA++; 

        if (dialStart != 0)
        {
            passwordB++;
        }
    }

    else if (dialStart != 0 && dialEnd != 0)
    {
        if ((direction == "L" && dialEnd > dialStart) || (direction == "R" && dialEnd < dialStart))
        {
            passwordB++;
        }
    }

    passwordB += distance / 100;

    dialStart = dialEnd;
 }

Console.WriteLine($"Part A: {passwordA}");
Console.WriteLine($"Part B: {passwordB}");
