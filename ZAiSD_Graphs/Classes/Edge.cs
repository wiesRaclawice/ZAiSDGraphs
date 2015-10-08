namespace ZAiSD_Graphs.Classes
{
    internal class Edge
    {
        private int _weight;
        public object _nodeId { get; }
        public Edge _nextEdge { get; }

        public Edge(int weight)
        {
            _weight = weight;
            _nextEdge = null;
        }

    }
}