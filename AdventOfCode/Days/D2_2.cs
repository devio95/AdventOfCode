using System;
using System.Linq;

namespace AdventOfCode.Days
{
    public class D2_2 : IAlgoritm
    {
        public string Run(string text)
        {
            string[] lines = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

            int result = 0;

            foreach (string line in lines)
            {
                int[] row = line.Split(' ').ToList().Select(x => int.Parse(x)).ToArray();

                if (CheckRow(row))
                {
                    result = result + 1;
                    continue;
                }
                else
                {
                    for (int i = 0; i < row.Length; i++)
                    {
                        int[] cuttedRow = row.Take(i).Concat(row.Skip(i + 1)).ToArray();
                        if (CheckRow(cuttedRow))
                        {
                            result = result + 1;
                            break;
                        }
                    }
                }
            }

            return result.ToString();
        }


        private bool CheckRow(int[] row)
        {
            if (row[0] == row[1])
            {
                return false;
            }

            Direction direction = row[0] > row[1] ? Direction.Down : Direction.Up;

            for (int i = 1; i < row.Length; i++)
            {
                int first = row[i - 1];
                int second = row[i];

                if (first == second)
                {
                    return false;
                }

                int abs = Math.Abs(first - second);
                if (abs <= 3 && abs >= 1)
                {
                    if ((direction == Direction.Up && first > second)
                        || (direction == Direction.Down && second > first))
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        private enum Direction
        {
            Up = 0,
            Down = 1
        }
    }
}