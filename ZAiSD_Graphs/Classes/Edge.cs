namespace ZAiSD_Graphs.Classes
{
    public class Edge
    {
        private int _weight;
        public Node NodeObject { get; }

        public Edge(int weight, Node node)
        {
            _weight = weight;
            NodeObject = node;
        }

    }
}