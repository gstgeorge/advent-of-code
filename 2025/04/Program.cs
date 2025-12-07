string[] file = File.ReadAllLines("data.txt");

char[][] data = [.. file.Select(x => x.ToCharArray())];

int partA = 0;
int partB = 0;

bool completeA = false;
bool completeB = false;

while(!completeB)
{
    int removals = 0;

    for (int i = 0; i < data.Length; i++)
    {
        for (int j = 0; j < data[i].Length; j++)
        {
            if (data[i][j] == '@')
            {
                int adj = 0;

                bool rowAbove = i > 0;
                bool rowBelow = i < data.Length - 1;
                bool colBefore = j > 0;
                bool colAfter = j < data[i].Length - 1;

                if (rowAbove && colBefore && data[i-1][j-1] == '@') { adj++; }
                if (rowAbove && data[i-1][j] == '@') { adj++; }
                if (rowAbove && colAfter && data[i-1][j+1] == '@') { adj++; }

                if (colBefore && data[i][j-1] == '@') { adj++; }
                if (colAfter && data[i][j+1] == '@') { adj++; }

                if (rowBelow && colBefore && data[i+1][j-1] == '@') { adj++; }
                if (rowBelow && data[i+1][j] == '@') { adj++; }
                if (rowBelow && colAfter && data[i+1][j+1] == '@') { adj++; }

                if (adj < 4) 
                {
                    if (!completeA) 
                    {
                        partA++; 
                    }

                    else
                    {
                        partB++;
                        removals++;
                        data[i][j] = '.';
                    }
                }
            }
        }
    }

    if (!completeA)
    {
        completeA = true;
        continue;
    }

    else if (removals == 0)
    {
        completeB = true;
    }
}

Console.WriteLine($"Part A: {partA}");
Console.WriteLine($"Part B: {partB}");
