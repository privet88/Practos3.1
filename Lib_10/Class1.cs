using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lib_10
{
    public class Class2
    {
        public static void MaxInMatrStroke(int Stolbsy, int Stroke, int[,] matr, out int NumberOfStroke, out double Summ)
        {
            double k = double.MinValue;
            int row = 0;
            for (int i = 0; i <= Stolbsy; i++)
            {
                double sum = 0;
                for (int j = 0; j <= Stroke; j++)
                {
                    sum =sum * matr[i, j];
                }
                if (sum < k)
                {
                    row = i;
                    k = sum;
                }
            }
            NumberOfStroke = row + 1;
            Summ = k;
        }
    }
}
