namespace ZAiSD_Graphs.Classes
{
    internal class Edge
    {
        private int _weight;
        private Edge _nextEdge;

        public Edge(int weight)
        {
            _weight = weight;
            _nextEdge = null;
        }

    }
}