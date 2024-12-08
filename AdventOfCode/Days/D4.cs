using System;

namespace AdventOfCode.Days
{
    public class D4 : IAlgoritm
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
            for (int row = 0; row < _rows; row++)
            {
                for (int column = 0; column < _columns; column++)
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
                foreach(char c in line)
                {
                    _chars[column, row] = c;
                    column++;
                }
                row++;
            }
        }

        private int Check()
        { 
            if (_chars[_column, _row] != 'X')
            {
                return 0;
            }

            int result = 0;
            bool right = Right();
            bool rightDown = RightDown();
            bool down = Down();
            bool downLeft = DownLeft();
            bool left = Left();
            bool leftUp = LeftUp();
            bool up = Up();
            bool rightUp = RightUp();

            result = right ? result + 1 : result;
            result = rightDown ? result + 1 : result;
            result = down ? result + 1 : result;
            result = downLeft ? result + 1 : result;
            result = left ? result + 1 : result;
            result = leftUp ? result + 1 : result;
            result = up ? result + 1 : result;
            result = rightUp ? result + 1 : result;

            return result;
        }

        private bool Right()
        {
            if (RightOk() == false)
            {
                return false;
            }

            return CheckText(_chars[_column + 1, _row], _chars[_column + 2, _row], _chars[_column + 3, _row]);
        }

        private bool RightDown()
        {
            if (RightOk() == false || DownOk() == false)
            {
                return false;
            }

            return CheckText(_chars[_column + 1, _row + 1], _chars[_column + 2, _row + 2], _chars[_column + 3, _row + 3]);
        }
        
        private bool Down()
        {
            if (DownOk() == false)
            {
                return false;
            }

            return CheckText(_chars[_column, _row + 1], _chars[_column, _row + 2], _chars[_column, _row + 3]);
        }

        private bool DownLeft()
        {
            if (DownOk() == false || LeftOk() == false)
            {
                return false;
            }

            return CheckText(_chars[_column - 1, _row + 1], _chars[_column - 2, _row + 2], _chars[_column - 3, _row + 3]);
        }

        private bool Left()
        {
            if (LeftOk() == false)
            {
                return false;
            }

            return CheckText(_chars[_column - 1, _row], _chars[_column - 2, _row], _chars[_column - 3, _row]);
        }

        private bool LeftUp()
        {
            if (LeftOk() == false || UpOk() == false)
            {
                return false;    
            }

            return CheckText(_chars[_column - 1, _row - 1], _chars[_column - 2, _row - 2], _chars[_column - 3, _row - 3]);
        }

        private bool Up()
        {
            if (UpOk() == false)
            {
                return false;
            }

            return CheckText(_chars[_column, _row - 1], _chars[_column, _row - 2], _chars[_column, _row - 3]);
        }

        private bool RightUp()
        {
            if (RightOk() == false || UpOk() == false)
            {
                return false;
            }

            return CheckText(_chars[_column + 1, _row - 1], _chars[_column + 2, _row - 2], _chars[_column + 3, _row - 3]);
        }

        private bool CheckText(char b, char c, char d)
        {
            char first = _chars[_column, _row];
            return first == 'X' && b == 'M' && c == 'A' && d == 'S';
        }

        private bool RightOk()
        {
            return _columns - _column >= 4;
        }

        private bool UpOk()
        {
            return _row >= 3;
        }

        private bool DownOk()
        {
            return _rows - _row >= 4;
        }

        private bool LeftOk()
        {
            return _column >= 3;
        }
    }
}