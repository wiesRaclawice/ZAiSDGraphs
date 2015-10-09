using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZAiSD_Graphs.Classes;

namespace ZAiSD_Graphs.Helpers
{
    public static class Weights
    {
        public static int SumWeightsTo(MyList<Edge> edges, Node node)
        {
            var sum = 0;
            foreach (var edge in edges)
            {
                if (edge.NodeTo.NodeId.Equals(node.NodeId)) sum += edge.Weight;
            }
            return sum;
        }
    }
}
