using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZAiSD_Graphs.Classes;
using ZAiSD_Graphs.Helpers;

namespace ZAiSD_Graphs
{
    class Program
    {
        static void Main(string[] args)
        {

            NeighborhoodRepresentation neighborhoodGraph = new NeighborhoodRepresentation(21);

            ReadFromFile.LoadNeighborhoodGraph(neighborhoodGraph);
            neighborhoodGraph.DeleteEdge(1,2);
            Console.WriteLine("ss");
        }
    }
}
