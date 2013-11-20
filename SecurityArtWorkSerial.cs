using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurityArtWorkSerial
{
    class Program
    {
        static void Main(string[] args)
        {
            Int16[] check = new Int16[] {0, 4, 6, 0, 6, 0, 0, 5, 6, 3, 0, 5, 6, 9, 2, 5};
            List<List<Int16>> res = new List<List<Int16>>();

            for (Int16 i = 0; i < 16; i++)
            {
                Console.Write("Posición " + i.ToString() + ": ");
                res.Add(new List<Int16>());
                for (Int16 j = 0; j < 10; j++)
                    if ((i * j) % 10 == check[i])
                    {
                        Console.Write(j.ToString() + " ");
                        res[i].Add(j);
                    }
                Console.WriteLine();
            }
            Genera(res, 0, "");
        }

        static int cnt = 0; //  16000 serials posibles

        static void Genera(List<List<Int16>> lista, int j, string num)
        {
            if (j < lista.Count)
                for (Int16 i = 0; i < lista[j].Count; i++)
                    Genera(lista, j + 1, num + lista[j][i].ToString());
            else
                Console.WriteLine((cnt++).ToString() + ":" + num);
        }
    }
}
