using System;
using System.Threading;

namespace ConsoleApp1
{
    internal class Program
    {
        private static Mutex mutex = new Mutex();
        static void Main(string[] args)
        {
            Thread thread1 = new Thread(ascendingOrder);
            Thread thread2 = new Thread(descendingOrder);

            thread1.Start();
            thread1.Join();

            thread2.Start();
            thread2.Join();

            Console.WriteLine("End");
            Console.ReadLine();
        }
        private static void ascendingOrder()
        {
            mutex.WaitOne();

            Console.WriteLine("Thread 1 started");

            for (int i = 0; i <= 20; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(200);
            }
            Console.WriteLine("Thread 1 end");
            mutex.ReleaseMutex();
        }
        private static void descendingOrder()
        {
            mutex.WaitOne();

            Console.WriteLine("Thread 2 started");
            for (int i = 10; i >= 0; i--)
            {
                Console.WriteLine(i);
                Thread.Sleep(200);
            }
            Console.WriteLine("Thread 2 end");
            mutex.ReleaseMutex();
        }

    }
}