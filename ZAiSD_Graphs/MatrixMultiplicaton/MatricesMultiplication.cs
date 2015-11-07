using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZAiSD_Graphs.MatrixMultiplicaton
{
    public class MatricesMultiplication
    {
        private List<MatrixWrapper> matrices;
        private int _numberOfThreads;
        private Tuple<int[,], int[,]> _results;
        private int[] _p;

        public MatricesMultiplication(String path)
        {
            matrices = new List<MatrixWrapper>();
            LoadMatrixes(path);
            InitP();
        }

        private void LoadMatrixes(String path)
        {

            var contents = File.ReadAllText(path);
            var matrixes = contents.Split(new[] {"\r\n\r\n"}, StringSplitOptions.RemoveEmptyEntries);

            foreach (var m in matrixes)
            {
                var rows = m.Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
                var x = rows.Length;
                var y = rows[0].Split(';').Length;
                var wrapper = new MatrixWrapper(x,y);
                wrapper.Add(rows);
                matrices.Add(wrapper);
            }
        }

        public MatrixWrapper Multiplicate(int numberOfThreads)
        {
            _numberOfThreads = numberOfThreads;
            if (_numberOfThreads > matrices.Count/2) throw new ApplicationException();

            Task<MatrixWrapper>[] taskArray = new Task<MatrixWrapper>[numberOfThreads];

            var num = matrices.Count/numberOfThreads; // 7/2 = 3
            var lastAddition = matrices.Count - numberOfThreads*num; // 7 - 2*3 = 1

            for (int i = 0; i < taskArray.Length; i++)
            {
                var range = matrices.GetRange(i*num, i == numberOfThreads - 1 ? num + lastAddition : num);
                taskArray[i] = new Task<MatrixWrapper>(() => DoWork(range));
                taskArray[i].Start();
            }

            Task.WaitAll(taskArray);
            return taskArray.Length > 1 ? DoWork(taskArray.Select(t => t.Result).ToList()) : taskArray[0].Result;
        }

        private MatrixWrapper DoWork(List<MatrixWrapper> range)
        {
            var partialResult = MultiplicateTwoMatrices(range[0], range[1]);
            if (range.Count <= 2) return partialResult;
            for (var i = 2; i < range.Count; i++)
            {
                partialResult = MultiplicateTwoMatrices(partialResult, range[i]);
            }

            return partialResult;

        }

        private MatrixWrapper MultiplicateTwoMatrices(MatrixWrapper matrixA, MatrixWrapper matrixB)
        {
            var result = new MatrixWrapper(matrixA.X, matrixB.Y);
            for (var i = 0; i < matrixA.X; i++)
            {
                for (var j = 0; j < matrixB.Y; j++)
                {
                   double value = 0;
                   for (var k = 0; k < matrixB.X; k++)
                    {
                        value += matrixA.Array[i, k] * matrixB.Array[k, j];
                    }
                    result.Array[i, j] = value;
                }
            }
            return result;
        }

        public void InitP()
        {
            _p = new int[matrices.Count + 1];

            _p[0] = matrices[0].X;
            _p[1] = matrices[0].Y;

            for (var i = 1; i <= matrices.Count; i++)
            {
                _p[i] = matrices[i - 1].Y;
            }
        }

        public MatrixWrapper MatrixChainMultiply()
        {
            var s = _results.Item2;

            return MultiplyOptimalParenthesis(matrices, s, 1, matrices.Count);
        }

        public MatrixWrapper ParallelMatrixChainMultiply()
        {
            var s = _results.Item2;

            Task<MatrixWrapper>[] taskArray = new Task<MatrixWrapper>[2];
            taskArray[0] = new Task<MatrixWrapper>(() => MultiplyOptimalParenthesis(matrices, s, 1, s[1, matrices.Count]));
            taskArray[0].Start();
            taskArray[1] = new Task<MatrixWrapper>(() => MultiplyOptimalParenthesis(matrices, s, s[1, matrices.Count] + 1, matrices.Count));
            taskArray[1].Start();

            Task.WaitAll(taskArray);

            return MultiplicateTwoMatrices(taskArray[0].Result, taskArray[1].Result);
        }

        public void ComputeMatrixChainOrder()
        {
            var n = _p.Length - 1; //matrices.Count
            var m = new int[n + 1, n + 1]; //cost
            var s = new int[n + 1, n + 1]; //which index?

            for (var i = 1; i <= n; i++)
            {
                m[i, i] = 0;
            }
            

            for (var l = 2; l <= n; l++) // for each chain length
            {
                for (var i = 1; i <= n - l + 1; i++)
                {
                    var j = i + l - 1;
                    m[i, j] = int.MaxValue;

                    for (   var k = i; k <= j - 1; k++)
                    {


                        var q = m[i,k] + m[k + 1,j] + (_p[i - 1] * _p[k] * _p[j]);
                        if (q < m[i, j])
                        {
                            m[i, j] = q;
                            s[i, j] = k;
                        }
                    }
                }
            }
            Console.WriteLine("Index it will be first deviding on: " + s[1, matrices.Count]);
            _results = Tuple.Create(m, s);
        }
        

        public MatrixWrapper MultiplyOptimalParenthesis(List<MatrixWrapper> matrices, int[,] s, int i, int j)
        {
            if (i == j) return matrices[i - 1];
            
            var matrix1 = MultiplyOptimalParenthesis(matrices, s, i, s[i, j]);
            var matrix2 = MultiplyOptimalParenthesis(matrices, s, s[i, j] + 1, j);
            return MultiplicateTwoMatrices(matrix1, matrix2);
        }

    }

}

