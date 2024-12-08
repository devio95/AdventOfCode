using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Days
{
    public class D3_2 : IAlgoritm
    {
        public string Run(string text)
        {
            string[] lines = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

            int result = 0;

            bool allow = true;
            string pattern = @"^mul\(\d{1,3},\d{1,3}\).*";
            string doPattern = @"^do\(\).*";
            string dontPattern = @"^don't\(\).*";

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                for (int j = 0; j < line.Count(); j++)
                {

                    string subtext = line.Substring(j);

                    if (Regex.IsMatch(subtext, doPattern))
                    {
                        allow = true;
                        continue;
                    }

                    if (Regex.IsMatch( subtext, dontPattern))
                    {
                        allow = false;
                        continue;
                    }

                    if (Regex.IsMatch(subtext, pattern))
                    {
                        if (allow)
                        {
                            int endIndex = subtext.IndexOf(')');
                            string expression = line.Substring(j, endIndex + 1);
                            expression = expression.Substring(4);
                            endIndex = expression.IndexOf(')');
                            expression = expression.Substring(0, endIndex);
                            int[] numbers = expression.Split(',').Select(x => int.Parse(x)).ToArray();
                            result += numbers[0] * numbers[1];
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return result.ToString();
        }
    }
}
