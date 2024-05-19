using System;
using System.Collections.Generic;
using System.Linq;
using Resources.Scripts.Utils;
using UnityEngine;

namespace Resources.Scripts.LevelGenerate
{
    public static class DirectionsOperations
    {
        public static Direction[][] GenerateDirectionsCombinations(int countDirections)
        {
            List<Direction> directions = new List<Direction>();
            foreach (var direction in Enum.GetNames(typeof(Direction)))
            {
                directions.Add(Enum.Parse<Direction>(direction));
            }
            if (countDirections >= 4)
            {
                return Combinations.GenerateCombinations(4, directions.ToArray());
            }

            if (countDirections <= 0)
            {
                return new Direction[][]{ };
            }

            return Combinations.GenerateCombinations(countDirections, directions.ToArray());
        }
        
        public static Direction GetOppositeDirection(Direction direction){
            switch (direction)
            {
                case Direction.Top:
                    return Direction.Bottom;
                case Direction.Right:
                    return Direction.Left;
                case Direction.Bottom:
                    return Direction.Top;
                case Direction.Left:
                    return Direction.Right;
                default:
                    throw new ArgumentException("Opposite direction doesn't exist");
            }
        }

        public static Quaternion GetRotation(Direction direction)
        {
            switch (direction)
            {
                case Direction.Bottom:
                    return Quaternion.Euler(0, 0, 0);
                case Direction.Left:
                    return Quaternion.Euler(0, 0, 90);
                case Direction.Top:
                    return Quaternion.Euler(0, 0, 180);
                case Direction.Right:
                    return Quaternion.Euler(0, 0, 270);
                default:
                    throw new ArgumentException("Rotation couldn't get");
            }
        }
    }
}
