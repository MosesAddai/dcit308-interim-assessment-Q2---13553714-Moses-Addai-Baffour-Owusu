using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        char[,] desert = {
            { '.', '.', '.', '.', '.' },
            { '.', 'S', '.', '.', '.' },
            { '.', '.', '.', 'E', '.' },
            { '.', '.', '.', '.', '.' }
        };

        int gas = 5;

        bool canReach = CanReachDestination(desert, gas);
        Console.WriteLine("Can reach destination? " + canReach);
    }

    static bool CanReachDestination(char[,] desert, int gas)
    {
        int rows = desert.GetLength(0);
        int cols = desert.GetLength(1);
        (int, int) start = (-1, -1), end = (-1, -1);

        // Locate Start (S) and End (E)
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (desert[r, c] == 'S') start = (r, c);
                if (desert[r, c] == 'E') end = (r, c);
            }
        }

        if (start == (-1, -1) || end == (-1, -1))
            return false;

        // BFS
        Queue<(int r, int c, int gasLeft)> q = new();
        HashSet<(int, int, int)> visited = new();

        q.Enqueue((start.Item1, start.Item2, gas));
        visited.Add((start.Item1, start.Item2, gas));

        int[] dr = { 1, -1, 0, 0 };
        int[] dc = { 0, 0, 1, -1 };

        while (q.Count > 0)
        {
            var (r, c, g) = q.Dequeue();

            if ((r, c) == end && g >= 0)
                return true;

            if (g == 0) continue;

            for (int i = 0; i < 4; i++)
            {
                int nr = r + dr[i];
                int nc = c + dc[i];

                if (nr >= 0 && nr < rows && nc >= 0 && nc < cols)
                {
                    var state = (nr, nc, g - 1);
                    if (!visited.Contains(state))
                    {
                        visited.Add(state);
                        q.Enqueue(state);
                    }
                }
            }
        }

        return false;
    }
}
