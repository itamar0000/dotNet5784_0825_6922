using System;
namespace Stage0
{
    partial class Program
    {
        static partial void Welcome6922();
        static void Main(string[] args)
        {
            Welcome0825();
            Welcome6922();

            Console.ReadKey();

        }

          private static void Welcome0825()
        { 

            Console.WriteLine("Enter your name:");
            string? username = Console.ReadLine();
            Console.WriteLine("{0},welcome to my first console application", username);
        }
    }
}