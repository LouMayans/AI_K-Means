using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace AI_K_Means
{
    public class Point
    {
        public int index;
        public Vector2 position = new Vector2(0, 0);
        public Cluster cluster;
    }
}
