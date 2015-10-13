using System;

namespace ZAiSD_Graphs.Classes
{
    public class Node
    {
        public int NodeId { get; }
        public MyList<Edge> EdgeList { get; }
        public Node ParentNode { get; set; }

        public Node(int nodeId)
        {
           NodeId = nodeId;
           EdgeList = new MyList<Edge>();
            ParentNode = null;
        }
    }
}