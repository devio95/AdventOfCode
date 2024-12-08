using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    public class D5_2 : IAlgoritm
    {
        Dictionary<int, List<int>> _beforeRules;
        Dictionary<int, List<int>> _afterRules;
        List<List<int>> _updates;
        public string Run(string text)
        {
            LoadData(text);
            int totalResult = 0;

            foreach (List<int> update in _updates)
            {
                if (IsUpdateCorrect(update) == false)
                {
                    List<int> correctUpdate = ConvertToCorrectUdpate(update);
                    int index = ((correctUpdate.Count + 1) / 2) - 1;
                    totalResult += correctUpdate[index];
                }
            }

            return totalResult.ToString();
        }

        private bool IsUpdateCorrect(List<int> update)
        {
            for (int i = 0; i < update.Count; i++)
            {
                for (int j = 0; j < update.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    if (i < j)
                    {
                        if (CompareBefore(update[i], update[j]) == false)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (CompareAfter(update[i], update[j]) == false)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private List<int> ConvertToCorrectUdpate(List<int> update)
        {
            List<int> toReturn = new List<int>();

            while (update.Count > 0)
            {
                int number = FindNext(update);
                toReturn.Add(number);

                update.Remove(number);
            }

            return toReturn;
        }

        private int FindNext(List<int> update)
        {
            for (int i = 0; i < update.Count; i++)
            {
                bool result = true;
                for (int j = 0; j < update.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    if (_afterRules.ContainsKey(update[i]) == false)
                    {
                        return update[i];
                    }

                    if (_afterRules[update[i]].Exists(x => x == update[j]))
                    {
                        result = false;
                        break;
                    }
                }

                if (result)
                {
                    return update[i];
                }
            }

            throw new Exception("Przeciez to nigdy nie nastąpi");
        }

        private void LoadData(string text)
        {
            string[] lines = text.Split(' ');
            LoadRules(lines[0]);
            LoadUpdates(lines[1]);
        }

        private bool CompareBefore(int before, int after)
        {
            if (_beforeRules.ContainsKey(after) == false)
            {
                return true;
            }
            return _beforeRules[after].Contains(before) == false;
        }

        private bool CompareAfter(int before, int after)
        {
            if (_afterRules.ContainsKey(after) == false)
            {
                return true;
            }
            return _afterRules[after].Contains(before) == false;
        }

        private void LoadRules(string text)
        {
            _beforeRules = new Dictionary<int, List<int>>();
            _afterRules = new Dictionary<int, List<int>>();

            string[] lines = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                int[] numbers = line.Split('|').Select(x => int.Parse(x)).ToArray();

                int before = numbers[0];
                int after = numbers[1];

                if (_beforeRules.ContainsKey(before) == false)
                {
                    _beforeRules[before] = new List<int>() { after };
                }
                else
                {
                    _beforeRules[before].Add(after);
                }

                if (_afterRules.ContainsKey(after) == false)
                {
                    _afterRules[after] = new List<int>() { before };
                }
                else
                {
                    _afterRules[after].Add(before);
                }
            }
        }

        private void LoadUpdates(string text)
        {
            _updates = new List<List<int>>();
            string[] lines = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                _updates.Add(line.Split(',').Select(x => int.Parse(x)).ToList());
            }
        }
    }
}