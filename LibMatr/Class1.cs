using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;


namespace LibMatr
{
    public class ClassMatr
    {
        public static void InflateMatr(int Stolbsy, int Stroke, int max, ref int[,] matr)//функция заполнения массива числами
        {

            Random rnd = new Random();

            for (int i = 0; i < Stolbsy; i++)
            {
                for (int j = 0; j < Stroke; j++)
                {
                    matr[i, j] = rnd.Next(1, max);
                }
            }
        }

        public static void ClearMatr(int Stolbsy, int Stroke, ref int[,] matr)//фунция очистки массива
        {

            for (int i = 0; i < Stolbsy; i++)
            {
                for (int j = 0; j < Stroke; j++)
                {
                    matr[i, j] = 0;
                }
            }
        }
    }
}
    

