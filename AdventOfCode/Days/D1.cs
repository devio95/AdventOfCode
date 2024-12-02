using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    public class D1 : IAlgoritm
    {
        private List<int> _left;
        private List<int> _right;
        public string Run(string text)
        {
            string[] lines = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

            _left = new List<int>();
            _right = new List<int>();
            foreach (string line in lines)
            {
                string[] columns = line.Split(',');
                _left.Add(int.Parse(columns[0]));
                _right.Add(int.Parse(columns[1]));
            }

            _left = _left.OrderBy(x => x).ToList();
            _right = _right.OrderBy(x => x).ToList();

            int sum = 0;

            for (int i = 0; i < _left.Count; i ++)
            {
                int left = _left[i];
                int right = _right[i];

                if (left == right)
                {
                    continue;
                }

                sum += left > right ? left - right : right - left;
            }
            return sum.ToString();
        }
    }
}