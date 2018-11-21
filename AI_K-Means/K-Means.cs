using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;


namespace AI_K_Means
{
    public static class K_Means
    {

        public static Cluster[] Cluster(Point[] points,  int numberOfClusters, float threshold)
        {
            int pointCount = points.GetLength(0);
            if (numberOfClusters > pointCount)
                throw new System.ArgumentException("Number of centroids exceed point size");

            //Connects the centroids to random points
            Cluster[] clusters = connectTheCentroidsToRandomPoints(numberOfClusters, points);

            int done = 0;
            int clusterIndex = 0;
            List<int> clusterDone = new List<int>();

            while(done != numberOfClusters)
            {
                if (clusterIndex == numberOfClusters)
                    clusterIndex = 0;

                if (clusterDone.Contains(clusterIndex))
                {
                    clusterIndex++;
                    continue;
                }
                
                bool clusterAddBool = clusters[clusterIndex].addPoint(threshold);
                bool clusterRemoveBool = clusters[clusterIndex].removePointThreshold(threshold);

                if (!clusterAddBool && !clusterRemoveBool)
                {
                    clusterDone.Add(clusterIndex);
                    ++done;
                }
                
                clusterIndex++;
            }

            return clusters;
        }


        private static Cluster[] connectTheCentroidsToRandomPoints(int numberOfCentroids, Point[] points)
        {
            Random rand = new Random();
            Cluster[] clusters = new Cluster[numberOfCentroids];
            for (int i = 0; i < numberOfCentroids; i++)
            {
                clusters[i] = new Cluster(points);
                clusters[i].clusterIndex = i;
                int random;
                do
                    random = rand.Next(points.GetLength(0));
                while (points[random].cluster != null);
                clusters[i].center = new Vector2( points[random].position.X, points[random].position.Y);
            }
            return clusters;
        }
    }
}

