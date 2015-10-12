﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            NeighborhoodRepresentation neighborhoodRepresentation = new NeighborhoodRepresentation(1000);
            ReadFromFile.LoadGraph(neighborhoodRepresentation, path);

            Warshall_Floyd alg = new Warshall_Floyd(neighborhoodRepresentation);
            alg.compute();
            long neighborhoodTime = alg.GetTimeElapsed();

            Console.WriteLine(alg.ShortestPathTable[109, 609]);
            var previous = alg.Previous;

            var secondToLast = previous[109, 609];
            do
            {
                Console.WriteLine(secondToLast);
                secondToLast = previous[109, secondToLast];
            } while (secondToLast != 109);

            MatrixRepresentation matrixRepresentation = new MatrixRepresentation(1000);
            ReadFromFile.LoadGraph(matrixRepresentation, path);
            alg = new Warshall_Floyd(matrixRepresentation);
            alg.compute();
            long matrixTime = alg.GetTimeElapsed();

            Console.WriteLine(alg.ShortestPathTable[109, 609]);
            previous = alg.Previous;

            secondToLast = previous[109, 609];
            do
            {
                Console.WriteLine(secondToLast);
                secondToLast = previous[109, secondToLast];
            } while (secondToLast != 109);


            Console.WriteLine("Time for the neighborhood:");
            Console.WriteLine(neighborhoodTime);

            Console.WriteLine("Time for the matrix:");
            Console.WriteLine(matrixTime);

            Console.Write("Ratio: ");
            Console.WriteLine((decimal)neighborhoodTime/matrixTime);



        }
    }
}
