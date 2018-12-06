using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace AI_K_Means
{
    class Program
    {
        static void Main(string[] args)
        {
            //Generates points
            Point[] points = PointsGenerator.GeneratePoints(15,5,5);
            float threshold = 2;

            //Writes the lines to the Console
            for (int i = 0; i < points.GetLength(0); i++)
                Console.WriteLine("Point " + (i+1) + " = [{0},{1}] ", points[i].position.X, points[i].position.Y);

            //Actually does the clustering
            Cluster[] clusters = K_Means.Cluster(points,2, threshold);

            //Console write the clusters and its points and distance to the centroid
            Console.WriteLine("\nthreshold = {0}", threshold);
            for (int clusterIndex = 0; clusterIndex < clusters.GetLength(0); clusterIndex++)
            {

                Console.WriteLine("Cluster " + clusterIndex + " [" + clusters[clusterIndex].center.X + "," + clusters[clusterIndex].center.X + "]" + " Has points:");
                for (int pointIndex = 0; pointIndex < clusters[clusterIndex].pointsInCluster.Count; pointIndex++)
                {
                    Console.Write("{2} [{0},{1}]",clusters[clusterIndex].pointsInCluster[pointIndex].position.X, clusters[clusterIndex].pointsInCluster[pointIndex].position.Y, clusters[clusterIndex].pointsInCluster[pointIndex].index + 1);
                    Console.Write("     Distance from centroid = {0}\n", Vector2.Distance(clusters[clusterIndex].center, clusters[clusterIndex].pointsInCluster[pointIndex].position));
                }
            }

            Console.ReadLine();
        }
    }
}
