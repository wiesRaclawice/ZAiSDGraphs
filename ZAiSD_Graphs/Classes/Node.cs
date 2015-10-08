using System;

namespace ZAiSD_Graphs.Classes
{
    internal class Node
    {
        public object NodeId { get; }
        public EdgeList EdgeList { get; }

        public Node(object nodeId)
        {
           NodeId = nodeId;
           EdgeList = new EdgeList();
        }
    }
}