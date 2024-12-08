using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    public class D7_2 : IAlgoritm
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
            GeneratePermutations(new List<MathType>(), count, toReturn);
            _permutations[count] = toReturn;
            return toReturn;
        }
        private void GeneratePermutations(List<MathType> current, int n, List<List<MathType>> result)
        {
            if (current.Count == n)
            {
                result.Add(new List<MathType>(current));
                return;
            }

            foreach (MathType value in Enum.GetValues(typeof(MathType)))
            {
                current.Add(value);
                GeneratePermutations(current, n, result);
                current.RemoveAt(current.Count - 1);
            }
        }


        private long Calculate(long a, long b, MathType mathType)
        {
            switch (mathType)
            {
                case MathType.Add:
                    return a + b;
                case MathType.Multiply:
                    return a * b;
                case MathType.Dynks:
                    return Dynks(a, b);
                default:
                    throw new NotImplementedException();
            }
        }

        private long Dynks(long a, long b)
        {
            string sa = a.ToString();
            string sb = b.ToString();

            return long.Parse($"{sa}{sb}");
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
            Multiply = 1,
            Dynks = 2
        }
    }
}