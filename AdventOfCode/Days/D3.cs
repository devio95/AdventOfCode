﻿using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Days
{
    public class D3 : IAlgoritm
    {
        public string Run(string text)
        {
            string[] lines = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

            int result = 0;

            string pattern = @"^mul\(\d{1,3},\d{1,3}\).*";
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                for (int j = 0; j < line.Count(); j++)
                {
                    string subtext = line.Substring(j);
                    if (Regex.IsMatch(subtext, pattern))
                    {
                        int endIndex = subtext.IndexOf(')');
                        string expression = line.Substring(j, endIndex + 1);
                        expression = expression.Substring(4);
                        endIndex = expression.IndexOf(')');
                        expression = expression.Substring(0, endIndex);
                        int[] numbers = expression.Split(',').Select(x => int.Parse(x)).ToArray();
                        result += numbers[0] * numbers[1];
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
