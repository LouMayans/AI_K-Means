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
            if (numberOfClusters > points.GetLength(0))
                throw new System.ArgumentException("Number of centroids exceed point size");

            //Connects the centroids to random points
            Cluster[] clusters = connectTheCentroidsToRandomPoints(numberOfClusters, points);
            int clusterIndex = 0;
            //adds the index of the cluster to this list to know what cluster is done.
            List<int> clusterDone = new List<int>();

            //Continues until all clusters are done clustering
            while(clusterDone.Count != numberOfClusters)
            {
                //if the last cluster already went then reset the index back to 0
                if (clusterIndex == numberOfClusters)
                    clusterIndex = 0;

                //if cluster is done then continue the loop
                if (clusterDone.Contains(clusterIndex))
                {
                    clusterIndex++;
                    continue;
                }
                
                //this section checks if the cluster did not add a point or removed a point
                //if not then the cluster is done.
                bool clusterAddBool = clusters[clusterIndex].addPoint(threshold);
                bool clusterRemoveBool = clusters[clusterIndex].removePointThreshold(threshold);
                if (!clusterAddBool && !clusterRemoveBool)
                    clusterDone.Add(clusterIndex);
                else
                    clusterDone = new List<int>();
                
                clusterIndex++;
            }

            return clusters;
        }

        //randomly selects and creates random clusters and sets them to a random point
        private static Cluster[] connectTheCentroidsToRandomPoints(int numberOfCentroids, Point[] points)
        {
            Random rand = new Random();
            Cluster[] clusters = new Cluster[numberOfCentroids];
            for (int i = 0; i < numberOfCentroids; i++)
            {
                clusters[i] = new Cluster(points);
                clusters[i].clusterIndex = i;
                int random;
                //it makes sure no N points are in more than 1 cluster
                do
                    random = rand.Next(points.GetLength(0));
                while (points[random].cluster != null);

                //sets the center of the cluster.
                clusters[i].center = new Vector2( points[random].position.X, points[random].position.Y);
            }
            return clusters;
        }
    }
}

