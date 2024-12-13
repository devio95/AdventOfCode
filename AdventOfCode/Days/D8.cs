using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AdventOfCode.Days
{
    public class D8 : IAlgoritm
    {
        private char[,] _array;
        private bool[,] _antyArray;
        private List<char> _distinctChars;
        public string Run(string text)
        {
            LoadData(text);

            foreach (char c in _distinctChars)
            {
                List<Point> points = GetPointsForChar(c);

                foreach (Point p1 in points)
                {
                    foreach (Point p2 in points)
                    {
                        if (p1 == p2)
                        {
                            continue;
                        }

                        CreateAnti(p1, p2);
                    }
                }
            }
            return GetResult();
        }

        private string GetResult()
        {
            int result = 0;
            for (int column = 0; column < _array.GetLength(0); column++)
            {
                for (int row = 0; row < _array.GetLength(1); row++)
                {
                    if (_antyArray[column, row] == true)
                    {
                        result++;
                    }

                }
            }

            return result.ToString();
        }

        private void CreateAnti(Point p1, Point p2)
        {
            Point upperPoint = p1.Y > p2.Y ? p2 : p1;
            Point lowerPoint = p1.Y > p2.Y ? p1 : p2;

            int rowDiff = Math.Abs(p1.Y - p2.Y);
            int colDiff = Math.Abs(p1.X - p2.X) ;

            Point upper = new Point(upperPoint.X > lowerPoint.X ? (upperPoint.X + colDiff) : (upperPoint.X - colDiff), upperPoint.Y - rowDiff);
            Point lower = new Point(upperPoint.X > lowerPoint.X ? (lowerPoint.X - colDiff) : (lowerPoint.X + colDiff), lowerPoint.Y + rowDiff);
            
            if (upper.X >= 0 && upper.Y >= 0 && upper.X < _antyArray.GetLength(0) && upper.Y < _antyArray.GetLength(1))
            {
                _antyArray[upper.X, upper.Y] = true;
            }

            if (lower.X >= 0 && lower.Y >= 0 && lower.X < _antyArray.GetLength(0) && lower.Y < _antyArray.GetLength(1))
            {
                _antyArray[lower.X, lower.Y] = true;
            }
        }

        private List<Point> GetPointsForChar(char c)
        {
            List<Point> toReturn = new List<Point>();

            for (int column = 0; column < _array.GetLength(0); column++)
            {
                for (int row = 0; row < _array.GetLength(1); row++)
                {
                    if (_array[column, row] == c)
                    {
                        toReturn.Add(new Point(column, row));
                    }
                }
            }

            return toReturn;
        }

        private void LoadData(string text)
        {
            string[] lines = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            _distinctChars = new List<char>();
            _array = new char[lines[0].Length, lines.Length];
            _antyArray = new bool[lines[0].Length, lines.Length];
            for (int row = 0; row < lines.Length; row++)
            {
                for (int column = 0; column < lines[row].Length; column++)
                {
                    char c = lines[row][column];
                    _array[column, row] = c;

                    if (_distinctChars.Exists(x => x == c) == false)
                    {
                        _distinctChars.Add(c);
                    }
                }
            }

            _distinctChars.RemoveAll(x => x == '.');
        }
    }
}