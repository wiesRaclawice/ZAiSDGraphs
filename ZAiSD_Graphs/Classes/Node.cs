using System;

namespace ZAiSD_Graphs.Classes
{
    internal class Node
    {
        public object NodeId { get; }
        public Edge _firstEdge { get; }

        public Node(object nodeId)
        {
            NodeId = nodeId;
            _firstEdge = null;
        }
    }
}