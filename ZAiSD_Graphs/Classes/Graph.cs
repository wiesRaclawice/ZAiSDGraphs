using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZAiSD_Graphs.Classes
{
    public interface Graph
    {
        Node[] GetNodes();
        MyList<Edge> GetEdges(); 
        void AddNode(int nodeId);
        void DeleteNode(int nodeId);
        void AddEdge(int nodeFrom, int nodeTo, int weight);
        void DeleteEdge(int fnoteFrom, int nodeTo);
        MyList<Node> GetNeighbors(int nodeId);
        MyList<Edge> GetOutboundEdges(int nodeId);
        MyList<Edge> GetIncidentEdges(int nodeId);
        Edge GetEdge(int nodeFrom, int nodeTo);
        int GetNumberOfNodes();
        int GetCurrentNumberOfNodes();
        int GetNumberOfEdges();
        Boolean areNeighbors(int firstNode, int secondNode);
        Boolean isEdge(int nodeFrom, int nodeTo);
    }
}
