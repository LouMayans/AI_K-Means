using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace AI_K_Means
{
    public class ClusterStack
    {
        public ClusterStack(ClusterAction action, Vector2 newCentroid)
        {
            clusterAction = action;
            centroid = newCentroid;
        }

        public ClusterStack(ClusterAction action, Point newPoint)
        {
            clusterAction = action;
            point = newPoint;
        }
        public ClusterAction clusterAction;
        public Point point;
        public Vector2 centroid;
    }

    public enum ClusterAction
    {
        Add,
        Remove,
        Centroid
    }
}
