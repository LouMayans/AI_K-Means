using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace AI_K_Means
{

    public class Cluster
    {
        List<ClusterStack> clusterStack;
        public int clusterIndex;
        public Vector2 center;
        public Point[] points;
        public List<Point> pointsInCluster;
        public List<float> distanceToPoints = new List<float>();

        public Cluster(Point[] allPoints)
        {
            points = allPoints;
            pointsInCluster = new List<Point>();
            clusterStack = new List<ClusterStack>();
        }
        public int getShortestDistance(float threshold)
        {
            CalculateNewDistances();

            float distance = -1;
            int shortest = -1;

            for (int i = 0; i < distanceToPoints.Count; i++)
            {
                if (distanceToPoints[i] <= threshold && distanceToPoints[i] >= 0)
                {
                    if (distance < 0)
                        distance = distanceToPoints[i];
                    else if (distanceToPoints[i] < distance)
                        distance = distanceToPoints[i];
                    else
                        continue;

                    shortest = i;
                }
            }

            return shortest;
        }

        public bool addPoint(float threshold)
        {
            int index = getShortestDistance(threshold);
            if (index == -1)
                return false;


            pointsInCluster.Add(points[index]);
            points[index].cluster = this;
            Console.WriteLine("Point {3} [{0},{1}] added to Cluster {2}", points[index].position.X, points[index].position.Y, clusterIndex, points[index].index + 1);
            clusterStack.Add(new ClusterStack(ClusterAction.Add, points[index]));
            CalculateCenter();

            return true;
        }

        public bool removePointThreshold(float threshold)
        {
            bool removed = false;
            for (int i = 0; i < pointsInCluster.Count; i++)
            {
                float distance = Vector2.Distance(pointsInCluster[i].position,center);
                if(distance > threshold)
                {
                    pointsInCluster[i].cluster = null;
                    pointsInCluster.Remove(pointsInCluster[i]);
                    Console.WriteLine("Point {3} [{0},{1}] removed from Cluster {2}", pointsInCluster[i].position.X, pointsInCluster[i].position.Y, clusterIndex, pointsInCluster[i].index + 1);
                    clusterStack.Add(new ClusterStack(ClusterAction.Remove, pointsInCluster[i]));
                    CalculateCenter();
                    removed = true;
                    i = 0;
                }
            }
            return removed;
        }
        public void CalculateNewDistances()
        {
            for (int i = 0; i < points.GetLength(0); i++)
            {
                if (distanceToPoints.Count <= i)
                    distanceToPoints.Add(new float());

                if (points[i].cluster == null)
                    distanceToPoints[i] = Vector2.Distance(center, points[i].position);
                else
                    distanceToPoints[i] = -1;
            }
        }
        private void CalculateCenter()
        {
            float x = 0, y = 0;
            foreach (Point point in pointsInCluster)
            {
                x += point.position.X;
                y += point.position.Y;
            }
            center = new Vector2(x / pointsInCluster.Count, y / pointsInCluster.Count);
            clusterStack.Add(new ClusterStack(ClusterAction.Centroid, center));
            CalculateNewDistances();
            Console.WriteLine("----New Cluster {0} centroid is [{1},{2}]", clusterIndex, center.X, center.Y);
        }
    }
}
