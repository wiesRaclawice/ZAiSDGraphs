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

        public Ford_Fulkerson(int source, int terminal, Graph representation)
        {
            _source = source;
            _terminal = terminal;
            _representation = representation;
            _stopwatch = new Stopwatch();
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
                    if (edge.Weight < minWeight)
                        minWeight = edge.Weight; 
                }

                //Dla każdej krawędzi (u,v) \in pf(u, v) \leftarrow f(u, v) +c_f(p) f(v, u) \leftarrow f(v, u) - c_f(p)
                ChangeWeights(path, minWeight);

                flow += minWeight;

                path = SearchPath(_representation.GetNodes()[_source], _representation.GetNodes()[_terminal]);
            }
            _stopwatch.Stop();
            
            Console.WriteLine("\n** Max flow = " + flow);
            Console.WriteLine("Time elapsed = " + _stopwatch.ElapsedMilliseconds);
            
        }

        private void ChangeWeights(MyList<Edge> path, long minWeight)
        {
            foreach (var edge in path)
            {
                if (_representation.GetType() == typeof (MatrixRepresentation))
                {
                    var rep = (MatrixRepresentation) _representation;
                    var edgeResidual = rep.Matrix[edge.NodeTo.NodeId, edge.NodeFrom.NodeId];
                    edge.Weight -= minWeight;
                }
                else
                {
                    var rep = (NeighborhoodRepresentation) _representation;
                    Edge edgeResidual = null;
                    foreach (var e in rep.GetOutboundEdges(edge.NodeTo.NodeId))
                    {
                        if (e.NodeFrom.NodeId.Equals(edge.NodeFrom.NodeId))
                        {
                            edgeResidual = e;
                            break;
                        }
                    }
                    edge.Weight -= minWeight;
                    if (edgeResidual != null)
                    {
                        edgeResidual.Weight += minWeight;
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

                var nodeEdges = _representation.GetOutboundEdges(current.NodeId);
                foreach (var edge in nodeEdges)
                {
                    var next = edge.NodeTo;
                    var weight = edge.Weight;
                    if (weight > 0 && !parsed.Contains(next))
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
                if (_representation.GetType() == typeof(MatrixRepresentation))
                {
                    var rep = (MatrixRepresentation) _representation;
                    path.Add(rep.Matrix[current.ParentNode.NodeId, current.NodeId]);
                }
                else
                {
                    var rep = (NeighborhoodRepresentation)_representation;
                    foreach (var e in rep.Nodes[current.ParentNode.NodeId].EdgeList)
                    {
                        if (e.NodeTo.NodeId.Equals(current.NodeId))
                        {
                            path.Add(e);
                            break;
                        }
                    }
                }
                
                current = current.ParentNode;
            }
            return path;
        }
    }
}
