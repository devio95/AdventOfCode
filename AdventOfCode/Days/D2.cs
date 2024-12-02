using System;
using System.Linq;

namespace AdventOfCode.Days
{
    public class D2 : IAlgoritm
    {
        public string Run(string text)
        {
            string[] lines = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

            int result = 0;

            foreach (string line in lines)
            {
                int[] row = line.Split(' ').ToList().Select(x => int.Parse(x)).ToArray();

                if (row[0] == row[1])
                {
                    continue;
                }

                Direction direction = row[0] > row[1] ? Direction.Down : Direction.Up;

                bool res = true;
                for (int i = 1; i < row.Length; i++)
                {
                    int first = row[i - 1];
                    int second = row[i];

                    if (first == second)
                    {
                        res = false;
                        break;
                    }

                    int abs = Math.Abs(first - second);
                    if (abs <= 3 && abs >= 1)
                    {
                        if ((direction == Direction.Up && first > second)
                            || (direction == Direction.Down && second > first))
                        {
                            res = false;
                            break;
                        }
                    }
                    else
                    {
                        res = false;
                        break;
                    }
                }

                if (res)
                {
                    result++;
                }
            }

            return result.ToString();
        }

        private enum Direction
        {
            Up = 0,
            Down = 1
        }
    }
}