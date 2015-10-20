using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZAiSD_Graphs.Classes;

namespace ZAiSD_Graphs.Algorithms
{
    public class Ford_Fulkerson
    {

        private int _source;
        private int _terminal;
        private Graph _representation;
        private Stopwatch _stopwatch;
        public long[,] FlowMatrix { get; }
        private int size;
        private Node[] nodes;

        public Ford_Fulkerson(int source, int terminal, Graph representation, int size)
        {
            _source = source;
            _terminal = terminal;
            _representation = representation;
            _stopwatch = new Stopwatch();
            FlowMatrix = new long[size,size];
            this.size = size;
            nodes = representation.GetNodes();
        }

        private void ResetFlowMatrix()
        {
            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    FlowMatrix[i,j] = 0;
                }
            }
        }

        public void compute()
        {
            _stopwatch.Start();
            long flow = 0;
            var path = SearchPath(_representation.GetNodes()[_source], _representation.GetNodes()[_terminal]);
            //Dopóki istnieje ścieżka p z s do t w grafie Gf  taka, że  cf(u,v)>0 dla wszystkich krawędzi p: 
            while (path != null && path.Length > 0)
            {
                long minWeight = long.MaxValue;

                //Znajdź c_f(p) = \min\{c_f(u,v) : (u,v) \in p\}
                foreach (var edge in path)
                {
                    var cf = edge.Weight - FlowMatrix[edge.NodeFrom.NodeId, edge.NodeTo.NodeId];
                    if (cf < minWeight)
                        minWeight = cf; 
                }

                //Dla każdej krawędzi (u,v) \in pf(u, v) \leftarrow f(u, v) +c_f(p) f(v, u) \leftarrow f(v, u) - c_f(p)
                ChangeWeights(path, minWeight);
                flow += minWeight;
                
                path = SearchPath(_representation.GetNodes()[_source], _representation.GetNodes()[_terminal]);
            }
            _stopwatch.Stop();
            Console.WriteLine("Time elapsed = " + _stopwatch.ElapsedMilliseconds);
            Console.WriteLine("Flow = " + flow);

        }

        private void ChangeWeights(MyList<Edge> path, long minWeight)
        {
            foreach (var edge in path)
            {
                FlowMatrix[edge.NodeFrom.NodeId, edge.NodeTo.NodeId] += minWeight;
                FlowMatrix[edge.NodeTo.NodeId, edge.NodeFrom.NodeId] -= minWeight;
                
                if (_representation.isEdge(edge.NodeTo.NodeId, edge.NodeFrom.NodeId))
                {
                    if (FlowMatrix[edge.NodeFrom.NodeId, edge.NodeTo.NodeId] > 0)
                    {
                        _representation.AddEdge(edge.NodeTo.NodeId,
                            edge.NodeFrom.NodeId, (int) FlowMatrix[edge.NodeFrom.NodeId, edge.NodeTo.NodeId]);
                    }
                }
            }
        }

        private MyList<Edge> SearchPath(Node source, Node terminal)
        {
            var queue = new Queue<Node>();
            var parsed = new HashSet<Node>();
            queue.Enqueue(source);

            while (queue.Count > 0)
            {
                Node current = queue.Dequeue();
                parsed.Add(current);
                if (current.NodeId.Equals(terminal.NodeId))
                {
                    return GetPath(current);
                }

                
                //Duży narzut dla macierzowej!!!
                var nodeEdges = _representation.GetOutboundEdges(current.NodeId);
                foreach (var edge in nodeEdges)
                {
                    var next = edge.NodeTo;
                    var weight = edge.Weight;
                    var curr = weight - FlowMatrix[edge.NodeFrom.NodeId, edge.NodeTo.NodeId];
                    if (curr > 0 && !parsed.Contains(next))
                    {
                        next.ParentNode = current;
                        queue.Enqueue(next);
                    }
                }
            }

            return null;
        }

        private MyList<Edge> GetPath(Node node)
        {
            var path = new MyList<Edge>();
            var current = node;
            while (current.ParentNode != null)
            {
                path.Add(_representation.GetEdge(current.ParentNode.NodeId, current.NodeId));
                current = current.ParentNode;
            }
            return path;
        }
    }
}
