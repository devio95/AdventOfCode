using System;
using System.Drawing;

namespace AdventOfCode.Days
{
    public class D6_2 : IAlgoritm
    {
        private FieldType[,] _startField;
        private Point _startStep;
        public string Run(string text)
        {
            LoadData(text);
            int result = 0;
            for (int column = 0; column < _startField.GetLength(0); column++)
            {
                for (int row = 0; row < _startField.GetLength(1); row++)
                {
                    if (_startField[column, row] == FieldType.NotVisited)
                    {
                        FieldType[,] field = CopyStartField();
                        field[column, row] = FieldType.Obstacle;
                        result = IsStuck(field) ? result + 1 : result;
                    }
                }
            }

            return result.ToString();
        }

        private FieldType[,] CopyStartField()
        {
            FieldType[,] toReturn = new FieldType[_startField.GetLength(0), _startField.GetLength(1)];

            for (int column = 0; column < _startField.GetLength(0); column ++)
            {
                for (int row = 0; row < _startField.GetLength(1); row++)
                {
                    toReturn[column, row] = _startField[column, row];
                }
            }

            return toReturn;
        }

        private bool IsStuck(FieldType[,] field)
        {
            int totalPossibleSteps = field.GetLength(0) * field.GetLength(1);
            int stepsCount = 0;
            Point currentStep = _startStep;
            Direction direction = Direction.Up;
            while (true)
            {
                if (stepsCount > totalPossibleSteps)
                {
                    return true;
                }
                Point nextStep = GetNextPoint(direction, currentStep);
                if (nextStep.X < 0 || nextStep.X == field.GetLength(0) || nextStep.Y < 0 || nextStep.Y == field.GetLength(1))
                {
                    break;
                }
                FieldType currentField = field[nextStep.X, nextStep.Y];
                if (currentField == FieldType.Obstacle)
                {
                    direction = GetNextDirection(direction);
                }
                else
                {
                    currentStep = nextStep;
                    field[currentStep.X, currentStep.Y] = FieldType.Visited;
                    stepsCount++;
                }
            }

            return false;
        }

        private Point GetNextPoint(Direction direction, Point step)
        {
            switch (direction)
            {
                case Direction.Up:
                    return new Point(step.X, step.Y - 1);
                case Direction.Right:
                    return new Point(step.X + 1, step.Y);
                case Direction.Down:
                    return new Point(step.X, step.Y + 1);
                case Direction.Left:
                    return new Point(step.X - 1, step.Y);
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

        private void LoadData(string text)
        {
            string[] lines = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

            _startField = new FieldType[lines[0].Length, lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    char c = lines[i][j];
                    if (c == '.')
                    {
                        _startField[j, i] = FieldType.NotVisited;
                    }
                    else if (c == '#')
                    {
                        _startField[j, i] = FieldType.Obstacle;
                    }
                    else if (c == '^')
                    {
                        _startField[j, i] = FieldType.Visited;
                        _startStep = new Point(j, i);
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