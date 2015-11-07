using System;
using System.Collections.Generic;
using System.Diagnostics;
using ZAiSD_Graphs.Algorithms;
using ZAiSD_Graphs.MatrixMultiplicaton;
using ZAiSD_Graphs.Classes;
using ZAiSD_Graphs.Helpers;

namespace ZAiSD_Graphs
{
    class Program
    {

        static void Main(string[] args)
        {
            var path = "C:\\Users\\Sabina\\Source\\Repos\\ZAiSD_Graphs\\ZAiSD_Graphs\\sample-matrices.txt";
            var path2  = "C:\\Users\\Sabina\\Source\\Repos\\ZAiSD_Graphs\\ZAiSD_Graphs\\matrices-smaller.txt";

            //var source = 109;
            //var terminal = 609;
            //var size = 1000;

            //MatrixRepresentation matrixRepresentation = new MatrixRepresentation(size);
            //ReadFromFile.LoadGraph(matrixRepresentation, path);

            //NeighborhoodRepresentation neighborhoodRepresentation = new NeighborhoodRepresentation(size);
            //ReadFromFile.LoadGraph(neighborhoodRepresentation, path);


            //Bellman_Ford bellman = new Bellman_Ford(matrixRepresentation, 109);

            //bellman.compute();

            //Console.WriteLine("MATRIX:");

            //PrintResults(bellman);

            //Console.WriteLine("NEIGHBORHOOD:");


            //bellman = new Bellman_Ford(neighborhoodRepresentation, 109);
            //bellman.compute();

            //PrintResults(bellman);
            Console.WriteLine("Matrix multiplication for 1 thread:");
            var st = new Stopwatch();
            var matricesMultiplicaton = new MatricesMultiplication(path);
            st.Start();
            matricesMultiplicaton.Multiplicate(1);
            st.Stop();
            Console.WriteLine("The operations took: " + st.ElapsedMilliseconds + " ms");

            PrintMultiplicationTimes(matricesMultiplicaton, path, 2, st.ElapsedMilliseconds);
            PrintMultiplicationTimes(matricesMultiplicaton, path, 10,st.ElapsedMilliseconds);
            PrintMultiplicationTimes(matricesMultiplicaton, path, 100, st.ElapsedMilliseconds);
            PrintMultiplicationTimes(matricesMultiplicaton, path, 200, st.ElapsedMilliseconds);
            PrintMultiplicationTimes(matricesMultiplicaton, path, 500, st.ElapsedMilliseconds);

            Console.WriteLine();
            Console.WriteLine("Non optimal for 400 matrices");

            matricesMultiplicaton = new MatricesMultiplication(path2);
            st.Restart();
            matricesMultiplicaton.Multiplicate(1);
            st.Stop();
            Console.WriteLine("The operations took: " + st.ElapsedMilliseconds + " ms");
            Console.WriteLine("Matrix multiplication with optimal parenthesis for 1 thread:");
            
            matricesMultiplicaton.ComputeMatrixChainOrder();
            st.Restart();
            matricesMultiplicaton.MatrixChainMultiply();
            st.Stop();
            var oneThreadTimeElapsed = st.ElapsedMilliseconds;
            Console.WriteLine("The operations took: " + oneThreadTimeElapsed + " ms");
            Console.WriteLine("Parallel matrix multiplication with optimal parenthesis:");
            st.Restart();
            matricesMultiplicaton.ParallelMatrixChainMultiply();
            st.Stop();
            Console.WriteLine("The operations took: " + st.ElapsedMilliseconds + " ms and it was " + (float)st.ElapsedMilliseconds/oneThreadTimeElapsed + " the time it took for 1 thread to finish");



            Console.ReadLine();
        }

        private static void PrintMultiplicationTimes(MatricesMultiplication matricesMultiplication, string path, int numberOfThreads, long milisecondsElapsed)
        {
            Console.WriteLine("Matrix multiplication for " + numberOfThreads + " threads:");
            var st = new Stopwatch();
            st.Restart();
            matricesMultiplication.Multiplicate(numberOfThreads);
            st.Stop();
            Console.WriteLine("The operations took: " + st.ElapsedMilliseconds + " ms and it was " + (float)st.ElapsedMilliseconds/milisecondsElapsed + " the time it took for 1 thread to finish");

        }

        private static void PrintResults(Bellman_Ford bellman)
        {
            Console.WriteLine("Path:");

            var previous = bellman.Previous;
            var secondToLast = previous[609];
            var stack = new Stack<int?>();
            do
            {
                stack.Push(secondToLast);
                secondToLast = previous[(int)secondToLast];
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

            Console.Write("[");
            for (int i = 0; i < bellman.Previous.Length; i++)
            {
                Console.Write(" " + bellman.ShortestPathTable[i] + " ");
            }
            Console.Write("]");
            Console.WriteLine();
        }
    }
}
