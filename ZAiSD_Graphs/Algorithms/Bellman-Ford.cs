using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZAiSD_Graphs.Classes;

namespace ZAiSD_Graphs.Algorithms
{
    class Bellman_Ford
    {
        private int NUMBER_OF_NODES;
        private Graph representation;
        public long[] ShortestPathTable { get; }
        public int?[] Previous { get; }
        private Stopwatch stopwatch;
        private long elapsedMiliseconds;
        private int _sourceId;

        public Bellman_Ford(Graph representation, int sourceId)
        {
            this.representation = representation;
            NUMBER_OF_NODES = representation.GetNumberOfNodes();
            ShortestPathTable = new long[NUMBER_OF_NODES];
            Previous = new int?[NUMBER_OF_NODES];
            stopwatch = new Stopwatch();
            _sourceId = sourceId;
        }

        public void compute()
        {
            stopwatch.Start();
            // Krok 1: inicjowanie struktur
            foreach (var node in representation.GetNodes())
            {
                if (node.NodeId.Equals(_sourceId))
                {
                    ShortestPathTable[node.NodeId] = 0;
                }
                else
                {
                    ShortestPathTable[node.NodeId] = int.MaxValue;
                }
                Previous[node.NodeId] = null;
            }

            var edges = representation.GetEdges();

            // Krok 2: relaksacyjne obliczanie długości ścieżek
            for (int i = 1; i < NUMBER_OF_NODES; i++)
            {
                var isChanged = false;
                foreach (var edge in edges)
                {
                    if (ShortestPathTable[edge.NodeFrom.NodeId].Equals(int.MaxValue)) continue;
                    
                    if (ShortestPathTable[edge.NodeFrom.NodeId] + edge.Weight < ShortestPathTable[edge.NodeTo.NodeId])
                    {
                        ShortestPathTable[edge.NodeTo.NodeId] = ShortestPathTable[edge.NodeFrom.NodeId] + edge.Weight;
                        Previous[edge.NodeTo.NodeId] = edge.NodeFrom.NodeId;
                        isChanged = true;
                    }
                }
                if (!isChanged) break;
            }

            // Krok 3: Sprawdzanie istnienia cykli o ujemnej długości
            foreach (var edge in edges)
            {
                if (ShortestPathTable[edge.NodeFrom.NodeId] + edge.Weight < ShortestPathTable[edge.NodeTo.NodeId])
                {
                    throw new System.ArgumentException("Graph contains a negative-weight cycle");
                }
            }

            stopwatch.Stop();
            Console.WriteLine("Time elapsed:");
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }

        
    }
}
