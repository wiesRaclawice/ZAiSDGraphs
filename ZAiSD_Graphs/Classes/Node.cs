using System;

namespace ZAiSD_Graphs.Classes
{
    public class Node
    {
        public int NodeId { get; }
        public MyList<Edge> EdgeList { get; }

        public Node(int nodeId)
        {
           NodeId = nodeId;
           EdgeList = new MyList<Edge>();
        }
    }
}