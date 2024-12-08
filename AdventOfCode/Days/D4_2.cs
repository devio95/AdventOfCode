using System;

namespace AdventOfCode.Days
{
    public class D4_2 : IAlgoritm
    {
        private char[,] _chars;
        private int _column;
        private int _row;
        private int _columns => _chars.GetLength(0);
        private int _rows => _chars.GetLength(1);

        public string Run(string text)
        {
            InsertData(text);
            int result = 0;
            for (int row = 1; row < _rows - 1; row++)
            {
                for (int column = 1; column < _columns - 1; column++)
                {
                    _column = column;
                    _row = row;
                    result += Check();
                }
            }

            return result.ToString();
        }

        private void InsertData(string text)
        {
            string[] lines = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            _chars = new char[lines[0].Length, lines.Length];
            int row = 0;
            int column = 0;
            foreach (string line in lines)
            {
                column = 0;
                foreach (char c in line)
                {
                    _chars[column, row] = c;
                    column++;
                }
                row++;
            }
        }

        private int Check()
        {
            if (_chars[_column, _row] != 'A')
            {
                return 0;
            }

            int result = One() + Two() + Three() + Four();

            return result >= 2 ? 1 : 0;
        }

        private int One()
        {
            bool result = CheckText(_chars[_column - 1, _row - 1], _chars[_column + 1, _row + 1]);
            return result ? 1 : 0;
        }

        private int Two()
        {
            bool result = CheckText(_chars[_column + 1, _row - 1], _chars[_column - 1, _row + 1]);
            return result ? 1 : 0;
        }

        private int Three()
        {
            bool result = CheckText(_chars[_column + 1, _row + 1], _chars[_column - 1, _row - 1]);
            return result ? 1 : 0;
        }

        private int  Four()
        {
            bool result = CheckText(_chars[_column - 1, _row + 1], _chars[_column + 1, _row - 1]);
            return result ? 1 : 0;
        }

        private bool CheckText(char a, char b)
        {
            char first = _chars[_column, _row];
            return first == 'A' && a == 'M' && b == 'S';
        }
    }
}