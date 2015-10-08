namespace ZAiSD_Graphs.Classes
{
    public class Edge
    {
        private int _weight;
        public Node NodeObject { get; }
        public Edge PreviousEdge;
        public Edge NextEdge { get; set; }

        public Edge(int weight, Node node)
        {
            _weight = weight;
            NodeObject = node;
            NextEdge = null;
            PreviousEdge = null;
        }

    }
}