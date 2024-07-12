using System;
using System.Threading;

namespace Task5
{
    internal class Program
    {
        private static Semaphore semaphore = new Semaphore(3, 3);
        private static Random random = new Random();

        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                int threadId = i;
                Thread thread = new Thread(() => generateNums(threadId));
                thread.Name = $"thread{threadId}";
                thread.Start();
            }

            Console.ReadLine();
        }
        private static void generateNums(object id)
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} is waiting to enter the semaphore.");
            semaphore.WaitOne();
            try
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} has entered the semaphore.");
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine(random.Next(1, 10));
                }
            }
            finally
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} is leaving the semaphore.");
                semaphore.Release();
            }


        }
    }
}
