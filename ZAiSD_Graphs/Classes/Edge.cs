namespace ZAiSD_Graphs.Classes
{
    public class Edge
    {
        public long Weight { get; set; }
        public Node NodeFrom { get; set; }
        public Node NodeTo { get; }

        public Edge(int weight, Node nodeFrom, Node nodeTo)
        {
            Weight = weight;
            NodeFrom = nodeFrom;
            NodeTo = nodeTo;
        }

    }
}