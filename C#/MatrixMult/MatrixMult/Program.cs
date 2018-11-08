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
        static Vector<double>[,] vA, vB, vC;
        static long n=500, id, iterations;

        static int threads = 8;
        private static bool MultiThreading = false;

        static void Main(string[] args)
        {
            args = "8,true  ".Split(',');

            int maxCores, minIOC;

            //parse settings
            bool multiThreading = false;
            var threadList = new List<int>();

            if (args.Length < 2)
                return;

            for (int i = 0; i < args.Length - 1; i++)
            {
                threadList.Add(int.Parse(args[i]));
            }
            multiThreading = Boolean.Parse(args[args.Length - 1]);
            //print settings
            Console.WriteLine($"multiThreading: {(multiThreading ? " On" : "Off")}");
            var s = string.Empty;
            threadList.ForEach(i => s += $"{i,3},");
            Console.WriteLine($"Test on {s} Threads");

            // Get the current ThreadPool-settings.
            ThreadPool.GetMinThreads(out maxCores, out minIOC);

            //run for each given setting
            foreach (var threads in threadList)
            {
                //run GC to avoid side effects from previous run
                GC.Collect();

                Console.WriteLine($"\n\n\n\nTest on {threads} Threads\n");

                //set CPU-affinity
                uint affinity = 0;
                if (multiThreading)
                    affinity = (uint)Math.Pow(2, threads) - 1;
                else
                    affinity = ((uint)Math.Pow(2, threads * 2) - 1) & 0x5555;

                var cores = CountBits(affinity);
                var highestCore = Math.Floor(Math.Log(affinity, 2)) + 1;

                if (highestCore > maxCores)
                {
                    Console.WriteLine($"This System has not {cores} physical Cores!");
                    return;
                }

                Process.GetCurrentProcess().ProcessorAffinity = (System.IntPtr)affinity;

                //set ThreadPool setting
                if (ThreadPool.SetMinThreads(threads, minIOC))
                {
                    MainAsync(args).GetAwaiter().GetResult();
                }
            }

            Console.ReadLine();
        }
        static async Task MainAsync(string[] args)
        {
            Console.WriteLine("Hello World!");


            //for (n = 100; n <= 2000; n += 100)
            //{
                id = 0;
                iterations = n * n;
                A = new double[n, n];
                B = new double[n, n];
                C = new double[n, n];
            vA = new Vector<double>[n, n];
            vB = new Vector<double>[n, n];
            vC = new Vector<double>[n, n];
            var random = new Random();
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        A[i, j] = random.NextDouble();
                        B[i, j] = random.NextDouble();
                        C[i, j] = 0.0;
                    vA[i, j] = new Vector<double>(random.NextDouble());
                    vB[i, j] = new Vector<double>(random.NextDouble());
                    vC[i, j] = new Vector<double>(0.0);
                }
                }
                var tasks = new List<Task>();


                var sw = Stopwatch.StartNew();
                if (!MultiThreading)
                    Mult(A, B, ref C, n);
                else
                {
                    for (int i = 0; i < threads; i++)
                    {
                        tasks.Add(Task.Factory.StartNew(HTMultVektor));
                    }
                    //run tasks
                    await Task.WhenAll(tasks);
                }
                    sw.Stop();

                Console.WriteLine($"{n}x{n}: {sw.ElapsedMilliseconds} ms");
                Console.WriteLine($"{n * n * n} ops / {sw.ElapsedMilliseconds} ms");
                Console.WriteLine($"{(n*n*n)/(sw.ElapsedMilliseconds * 1000000.0)} GFLOPS");
                Console.WriteLine();
                

            //}
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void HTMult()
        {
            long value;
            while (id < iterations)
            {
                value = Interlocked.Increment(ref id);
                if (value >= iterations)
                    break;

                var i = value / n;
                var k = value % n;
                double cik = 0.0;
                for (int j = 0; j < n; j++)
                {
                    cik += A[i, j] * B[j, k];
                }
                C[i, k] = cik;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void HTMultVektor()
        {
            long value;
            while (id < iterations)
            {
                value = Interlocked.Increment(ref id);
                if (value >= iterations)
                    break;

                var i = value / n;
                var k = value % n;
                Vector<double> cik = new Vector<double>(0.0);
                for (int j = 0; j < n; j++)
                {
                    cik += vA[i, j] * vB[j, k];
                }
                vC[i, k] = cik;
            }
        }

        public static int CountBits(uint value)
            {
                int count = 0;
                while (value != 0)
                {
                    count++;
                    value &= value - 1;
                }
                return count;
            }
        }
}
