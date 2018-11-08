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
        static long n = 500, id;


        static void Main(string[] args)
        {

            Console.WriteLine("Hello World!");


            for (n = 100; n <= 2000; n += 100)
            {
                id = 0;
                A = new double[n, n];
                B = new double[n, n];
                C = new double[n, n];
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
                var tasks = new List<Task>();


                var sw = Stopwatch.StartNew();
                Mult(A, B, ref C, n);
                sw.Stop();

                Console.WriteLine($"{n}x{n}: {sw.ElapsedMilliseconds} ms");
                Console.WriteLine($"{n * n * n} ops / {sw.ElapsedMilliseconds} ms");
                Console.WriteLine($"{(n * n * n) / (sw.ElapsedMilliseconds * 1000000.0)} GFLOPS");
                Console.WriteLine();


            }
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
