namespace ZAiSD_Graphs.Classes
{
    public class Edge
    {
        private int _weight;
        public object _nodeId { get; }
        public Edge _PreviousEdge;
        public Edge _nextEdge { get; set; }

        public Edge(int weight, object nodeId)
        {
            _weight = weight;
            _nodeId = nodeId;
            _nextEdge = null;
        }

    }
}