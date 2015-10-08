namespace ZAiSD_Graphs.Classes
{
    public class EdgeList
    {
        public Edge Head { get; set; }
        public Edge Tail { get; set; }

        public EdgeList()
        {
            Head = null;
            Tail = null;
        }
    }
}