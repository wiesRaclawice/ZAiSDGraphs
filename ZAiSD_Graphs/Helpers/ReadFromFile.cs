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

        public static void LoadGraph(Graph representation, string path)
        {
            try
            {
                using (var sr = new StreamReader(path))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var values = line.Split(';', ' ');
                        representation.AddEdge(Convert.ToInt32(values[0]), Convert.ToInt32(values[2]),
                            Convert.ToInt32(values[4]));
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
