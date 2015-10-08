using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZAiSD_Graphs.Classes
{
    interface Graph
    {
        void AddNode(Object nodeId);
        void DeleteNode(Object nodeId);
        void AddEdge(object firstNode, object secondNode, int weight);
        void DeleteEdge(Object firstNode, Object secondNode);
        List<int> GetNeighbors(Object nodeId);
        List<int> GetEdges(Object nodeId);
        Int32 GetNumberOfNodes();
        Int32 GetNumberOfEdges();
        Boolean areNeighbors(Object firstNode, Object secondNode); 

    }
}
