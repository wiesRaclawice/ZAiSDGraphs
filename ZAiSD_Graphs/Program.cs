using System;
using System.Collections.Generic;
using ZAiSD_Graphs.Algorithms;
using ZAiSD_Graphs.Classes;
using ZAiSD_Graphs.Helpers;

namespace ZAiSD_Graphs
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = "C:\\Users\\Sabina\\Source\\Repos\\ZAiSD_Graphs\\ZAiSD_Graphs\\graf_duzy.txt";

            /*MatrixRepresentation matrixRepresentation = new MatrixRepresentation(1000);
            ReadFromFile.LoadMatrixGraph(matrixRepresentation);
            Console.WriteLine(matrixRepresentation.GetNumberOfNodes());*/

            var source = 109;
            var terminal = 609;
            var size = 1000;

            MatrixRepresentation matrixRepresentation = new MatrixRepresentation(size);
            ReadFromFile.LoadGraph(matrixRepresentation, path);

            /*Ford_Fulkerson alg = new Ford_Fulkerson(source,terminal,matrixRepresentation, size);
            alg.compute();

            NeighborhoodRepresentation neighborhoodRepresentation = new NeighborhoodRepresentation(size);
            ReadFromFile.LoadGraph(neighborhoodRepresentation, path);

            alg = new Ford_Fulkerson(source,terminal,neighborhoodRepresentation, size);
            alg.compute();*/

            Bellman_Ford bellman = new Bellman_Ford(matrixRepresentation, 109);
            bellman.compute();

            Console.WriteLine("Path:");

            var previous = bellman.Previous;
            var secondToLast = previous[609];
            var stack = new Stack<int?>();
            do
            {
                stack.Push(secondToLast);
                secondToLast = previous[(int) secondToLast];
            } while (secondToLast != 109);

            Console.Write("[ 109 ");
            while (stack.Count != 0)
            {
                Console.Write(stack.Pop());
                Console.Write(" ");
            }
            Console.WriteLine("609 ]");

            Console.WriteLine("Distance:");
            Console.WriteLine(bellman.ShortestPathTable[609]);



        }
    }
}
