using System;
using System.Linq;
using System.Threading;

namespace Task2
{
    internal class Program
    {
        private static Mutex mutex = new Mutex(false);

        static void Main(string[] args)
        {
            int[] nums = { 1, 2, 3, 4, 5 };

            Random rand = new Random();
            int num = rand.Next(1, 5);

            for (int i = 0; i < nums.Length; i++)
            {
                Console.WriteLine(nums[i]);
            }

            Console.WriteLine("Increment: " + num);

            Console.WriteLine("Press enter");
            Console.ReadLine();

            Thread thread1 = new Thread(() => increaseValue(nums, num));
            Thread thread2 = new Thread(() => findMax(nums, num));

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            Console.WriteLine("New array: ");
            for (int i = 0; i < nums.Length; i++)
            {
                Console.WriteLine(nums[i]);
            }

            Console.ReadLine();
        }

        private static void increaseValue(int[] nums, int num)
        {
            mutex.WaitOne();
            Console.WriteLine("Thread 1 started");

            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] += num;
            }

            Console.WriteLine("Thread 1 end");
            mutex.ReleaseMutex();
        }
        private static void findMax(int[] nums, int num)
        {
            mutex.WaitOne();
            Console.WriteLine("Thread 2 started");

            Console.WriteLine("Max num: " + nums.Max());

            Console.WriteLine("Thread 2 end");
            mutex.ReleaseMutex();
        }
    }
}
