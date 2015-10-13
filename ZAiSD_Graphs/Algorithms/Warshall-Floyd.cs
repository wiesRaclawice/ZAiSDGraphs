using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using ZAiSD_Graphs.Classes;

namespace ZAiSD_Graphs.Algorithms
{
    public class Warshall_Floyd
    {
        private int NUMBER_OF_NODES;
        private Graph representation;
        public long[,] ShortestPathTable { get; }
        public int[,] Previous { get; }
        private Stopwatch stopwatch;
        private long elapsedMiliseconds;

        public Warshall_Floyd(Graph representation)
        {
            this.representation = representation;
            NUMBER_OF_NODES = representation.GetNumberOfNodes();
            ShortestPathTable = new long[NUMBER_OF_NODES,NUMBER_OF_NODES];
            Previous = new int[NUMBER_OF_NODES,NUMBER_OF_NODES];
            stopwatch = new Stopwatch();
        }

        public void compute()
        {
            stopwatch.Restart();

            for (int i = 0; i < NUMBER_OF_NODES; i++)
            {
                for (var j = 0; j < NUMBER_OF_NODES; j++)
                {
                    ShortestPathTable[i, j] = int.MaxValue;
                    Previous[i, j] = -1;
                }
                ShortestPathTable[i, i] = 0;
            }

            if (representation.GetType() == typeof (MatrixRepresentation))
            {
                ComputeMatrix();
            }
            else
            {
                ComputeNeighborhood();
            }

            for (int i = 0; i < NUMBER_OF_NODES; i++)
            {
                for (var j = 0; j < NUMBER_OF_NODES; j++)
                {
                    for (var k = 0; k < NUMBER_OF_NODES; k++)
                    {
                        if (ShortestPathTable[j, i] == int.MaxValue || ShortestPathTable[i, k] == int.MaxValue) continue;

                        if (ShortestPathTable[j, k] > ShortestPathTable[j, i] + ShortestPathTable[i, k])
                        {
                            ShortestPathTable[j, k] = ShortestPathTable[j, i] + ShortestPathTable[i, k];
                            Previous[j, k] = Previous[i, k];
                        }
                    }
                }
            }

            stopwatch.Stop();
            elapsedMiliseconds = stopwatch.ElapsedMilliseconds;

        }

        private void ComputeMatrix()
        {

            MatrixRepresentation matrixRepresentation = (MatrixRepresentation) representation;
            
            for (int i = 0; i < NUMBER_OF_NODES; i++)
            {
                for (var j = 0; j < NUMBER_OF_NODES; j++)
                {
                    var edge = matrixRepresentation.Matrix[i, j];
                    if (edge != null)
                    {
                        ShortestPathTable[i, j] = edge.Weight;
                        Previous[i, j] = i;
                    }
                }
            }

        }

        private void ComputeNeighborhood()
        {
            NeighborhoodRepresentation neighborhoodRepresentation = (NeighborhoodRepresentation) representation;

            var nodes = neighborhoodRepresentation.Nodes;
            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i].EdgeList != null)
                {
                    foreach (var edge in nodes[i].EdgeList)
                    {
                        ShortestPathTable[i, edge.NodeTo.NodeId] = edge.Weight;
                        Previous[i, edge.NodeTo.NodeId] = i;
                    }
                }
                
            }


        }


        public long GetTimeElapsed()
        {
            return elapsedMiliseconds;
        }


    }
}
