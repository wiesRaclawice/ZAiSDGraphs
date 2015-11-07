using System;
using System.Globalization;

namespace ZAiSD_Graphs.MatrixMultiplicaton
{
    public class MatrixWrapper
    {
        public double[,] Array { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        
        public MatrixWrapper(int x, int y)
        {
            X = x;
            Y = y;
            Array = new double[x, y];
        }

        public void Add(String[] rows)
        {
            for (var i = 0; i < X; i++)
            {
                var columnValues = rows[i].Split(';');
                for (var j = 0; j < Y; j++)
                {
                    Array[i, j] = Convert.ToDouble(columnValues[j], CultureInfo.InvariantCulture);
                }
            }
        }
    }
}
