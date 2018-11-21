using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Numerics;
using System.Threading.Tasks;

namespace AI_K_Means
{
    public static class PointsGenerator
    {
        public static Point[] GeneratePoints(int numberOfPoints, int maxX = 20, int maxY = 20, int seed = 0)
        {
            if (maxX * maxY <= numberOfPoints)
                numberOfPoints = maxX * maxY - 1;

            Console.WriteLine("number of points generated {0}", numberOfPoints);

            Random rand;
            if (seed == 0)
                rand = new Random();
            else
                rand = new Random(seed);

            Point[] points = new Point[numberOfPoints];

            for (int i = 0; i < numberOfPoints; i++)
            {
                points[i] = new Point();
                points[i].index = i;
            }
            //Iterates through from 0 to number of points to assign a random unique position to the array.
            for (int i = 0; i < numberOfPoints; i++)
            {
                bool alreadyAPoint;
                Vector2 newPosition;

                //checks if the newly generated position is in the array already.
                do
                {
                    alreadyAPoint = false;
                    newPosition = new Vector2(rand.Next(maxX), rand.Next(maxY));
                    for (int j = 0; j < points.GetLength(0); j++)
                    {
                        if (points[j].position == newPosition)
                            alreadyAPoint = true;
                    }
                } while (alreadyAPoint);

                points[i].position = newPosition;
            }
            return points;
        }
    }
}
