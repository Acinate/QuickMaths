using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuickMaths
{
    class Program
    {
        public static List<string> logs = new List<string>();

        static void Main(string[] args)
        {
            Run();
            Console.ReadLine();
        }

        public static void Run()
        {
            int[] numberList = GenerateIntRange(1, 100);
            int[] oddNumbers = ReturnOddEvenArrays(numberList)[0];
            int[] evenNumbers = ReturnOddEvenArrays(numberList)[1];
            Task printOdd = Task.Factory.StartNew(() => PrintNumberArray("Thread 1", oddNumbers));
            Task printEven = Task.Factory.StartNew(() => PrintNumberArray("Thread 2", evenNumbers));
            Task.WaitAll(printOdd, printEven);
        }

        public static Thread LastRunningThread = null;

        public static void PrintNumberArray(string threadName, int[] arr)
        {
            for (int i=0;i<arr.Length; i++)
            {
                if (LastRunningThread != Thread.CurrentThread)
                {
                    Console.WriteLine(threadName + ": The number is '" + arr[i] + "' " + GetNumberProperty(arr[i]));
                    LastRunningThread = Thread.CurrentThread;
                }
                else
                {
                    i -= 1;
                    Thread.Yield();
                }
            }
        }

        public static string GetNumberProperty(int number)
        {
            if (number % 3 == 0 && number % 2 == 0)
            {
                return "is divisible by two and three.";
            } else if (number % 3 == 0)
            {
                return "is divisible by three.";
            } else if (number % 2 == 0)
            {
                return "is even.";
            } else
            {
                return "is odd.";
            }
        }

        public static int[] GenerateIntRange(int start, int end)
        {
            List<int> tempList = new List<int>();
            for (int i=start;i<=end;i++)
            {
                tempList.Add(i);
            }
            return tempList.ToArray();
        }

        public static int[][] ReturnOddEvenArrays(int[] arr)
        {
            List<int> oddArray = new List<int>();
            List<int> evenArray = new List<int>();
            for (int i=0;i<arr.Length;i++)
            {
                if (NumberIsEven(arr[i]))
                {
                    evenArray.Add(arr[i]);
                } else
                {
                    oddArray.Add(arr[i]);
                }
            }
            return new int[][] { oddArray.ToArray(), evenArray.ToArray() };
        }

        public static bool NumberIsEven(int number)
        {
            return number % 2 == 0;
        }
    }
}
