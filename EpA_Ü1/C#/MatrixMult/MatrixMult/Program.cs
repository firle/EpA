using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace MatrixMult
{
    class Program
    {
        static double[,] A, B, C;


        static void Main(string[] args)
        {

            Console.WriteLine("Hello World!");


            A = new double[2000, 2000];
            B = new double[2000, 2000];
            C = new double[2000, 2000];

            for (int n = 100; n <= 2000; n += 100)
            {
                var random = new Random();
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        A[i, j] = random.NextDouble();
                        B[i, j] = random.NextDouble();
                        C[i, j] = 0.0;
                    }
                }

                GC.Collect();


                var sw = Stopwatch.StartNew();
                Mult(A, B, ref C, n);
                sw.Stop();

                Console.WriteLine($"{n}x{n}: {sw.ElapsedMilliseconds} ms");
                Console.WriteLine();


            }

            Console.ReadLine();

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Mult(double[,] A, double[,] B, ref double[,] C, long n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int k = 0; k < n; k++)
                {
                    double cik = 0.0;
                    for (int j = 0; j < n; j++)
                    {
                        cik += A[i, j] * B[j, k];
                    }
                    C[i, k] = cik;
                }
            }
        }
    }
}
