using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZAiSD_Graphs.Classes;

namespace ZAiSD_Graphs.Helpers
{
    public static class ReadFromFile
    {
        public static void LoadNeighborhoodGraph(NeighborhoodRepresentation neighborhoodGraph)
        {
            try
            {
                using (StreamReader sr = new StreamReader("C:\\Users\\Sabina\\Source\\Repos\\ZAiSD_Graphs\\ZAiSD_Graphs\\graf.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] values = line.Split(';', ' ');
                        neighborhoodGraph.AddEdge(Convert.ToInt32(values[0]), Convert.ToInt32(values[2]),
                            Convert.ToInt32(values[4]));
                        Console.WriteLine("Edge added");
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("File not found or couldn't be read.");
            }
        }

        public static void LoadMatrixGraph(MatrixRepresentation matrixRepresentation)
        {
            try
            {
                using (StreamReader sr = new StreamReader("C:\\Users\\Sabina\\Source\\Repos\\ZAiSD_Graphs\\ZAiSD_Graphs\\graf.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] values = line.Split(';', ' ');
                        matrixRepresentation.AddEdge(Convert.ToInt32(values[0]), Convert.ToInt32(values[2]), Convert.ToInt32(values[4])); 
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("File not found or couldn't be read.");
            }
        }
        
    }
}
