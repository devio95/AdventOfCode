using System;
using System.Diagnostics;
using System.Drawing;

namespace AdventOfCode.Days
{
    public class D6 : IAlgoritm
    {
        private FieldType[,] _field;
        private Point _step;
        public string Run(string text)
        {
            LoadData(text);

            Direction direction = Direction.Up;
            while(true)
            {
                Point nextStep = GetNextPoint(direction);
                if (nextStep.X < 0 || nextStep.X == _field.GetLength(0) || nextStep.Y < 0 || nextStep.Y == _field.GetLength(1))
                {
                    break;
                }
                FieldType currentField = _field[nextStep.X, nextStep.Y];
                if (currentField == FieldType.Obstacle)
                {
                    direction = GetNextDirection(direction);
                }
                else
                {
                    _step = nextStep;
                    _field[_step.X, _step.Y] = FieldType.Visited;
                }
            }

            return Result();
        }

        private Point GetNextPoint(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return new Point(_step.X, _step.Y - 1);
                case Direction.Right:
                    return new Point(_step.X + 1, _step.Y);
                case Direction.Down:
                    return new Point(_step.X, _step.Y + 1);
                case Direction.Left:
                    return new Point(_step.X - 1, _step.Y);
                default:
                    throw new Exception();
            }
        }

        private Direction GetNextDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return Direction.Right;
                case Direction.Right:
                    return Direction.Down;
                case Direction.Down:
                    return Direction.Left;
                case Direction.Left:
                    return Direction.Up;
                default:
                    throw new Exception();
            }
        }

        private string Result()
        {
            int result = 0;
            for (int i = 0; i < _field.GetLength(0); i++)
            {
                for (int j = 0; j < _field.GetLength(1); j++)
                {
                    if (_field[i, j] == FieldType.Visited)
                    {
                        result++;
                    }
                }
            }
            return result.ToString();
        }

        private void LoadData(string text)
        {
            string[] lines = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

            _field = new FieldType[lines[0].Length, lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    char c = lines[i][j];
                    if (c == '.')
                    {
                        _field[j, i] = FieldType.NotVisited;
                    }
                    else if (c == '#')
                    {
                        _field[j, i] = FieldType.Obstacle;
                    }
                    else if (c == '^')
                    {
                        _field[j, i] = FieldType.Visited;
                        _step = new Point(j, i);
                    }
                }
            }
        }

        private enum FieldType
        {
            Obstacle = 0,
            Visited = 1,
            NotVisited = 2
        }

        private enum Direction
        {
            Up = 0,
            Right = 1,
            Down = 2,
            Left = 3
        }
    }
}