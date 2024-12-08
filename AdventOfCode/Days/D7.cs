using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    public class D7 : IAlgoritm
    {
        private Dictionary<long, List<int>> _equations;
        private Dictionary<int, List<List<MathType>>> _permutations;
        public string Run(string text)
        {
            LoadData(text);
            long result = 0;
            foreach (var equation in _equations)
            {
                if (OkEquation(equation))
                {
                    result += equation.Key;
                }
            }

            return result.ToString();
        }

        private bool OkEquation(KeyValuePair<long, List<int>> equation)
        {
            List<List<MathType>> possible = GetPossibleMathCombinations(equation.Value.Count - 1);
            foreach (List<MathType> math in possible)
            {
                long result = 0;
                MathType mathType = math[0];

                result = Calculate(equation.Value[0], equation.Value[1], mathType);

                for (int i = 2; i < equation.Value.Count; i++)
                {
                    result = Calculate(result, equation.Value[i], math[i - 1]);
                }

                if (result == equation.Key)
                {
                    return true;
                }
            }

            return false;
        }

        private List<List<MathType>> GetPossibleMathCombinations(int count)
        {
            if (_permutations.ContainsKey(count))
            {
                return _permutations[count];
            }

            List<List<MathType>> toReturn = new List<List<MathType>>();
            int possiblePermutationsCount = (int)Math.Pow(2, count);

            for (int i = 0; i < possiblePermutationsCount; i++)
            {
                List<MathType> combination = new List<MathType>();

                bool[] bools = new BitArray(new int[] { i }).Cast<bool>().ToArray().Take(count).ToArray();

                for (int b = 0; b < bools.Count(); b++)
                {
                    combination.Add(bools[b] == true ? MathType.Add : MathType.Multiply);
                }
                toReturn.Add(combination);
            }

            _permutations[count] = toReturn;
            return toReturn;
        }

        private long Calculate(long a,long b, MathType mathType)
        {
            return mathType == MathType.Add ? a + b :
                    a * b;
        }

        private void LoadData(string text)
        {
            _permutations = new Dictionary<int, List<List<MathType>>>();
            string[] lines = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            _equations = new Dictionary<long, List<int>>();
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] values = line.Split(':');
                long value = long.Parse(values[0]);
                _equations[value] = new List<int>();
                int[] v2 = values[1].Split(' ').Where(x => x != "").Select(y => int.Parse(y)).ToArray();
                _equations[value].AddRange(v2);
            }
        }

        private enum MathType
        {
            Add = 0,
            Multiply = 1
        }
    }
}