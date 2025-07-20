// See https://aka.ms/new-console-template for more information
using System;
namespace CALCULATOR
{
    internal class Program
{
        static void Main(string[] arrays)
        {
            Console.WriteLine("enter the first number");
            int a = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("enter the first number");
            int b = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("the sum is :" + (a + b));
            Console.WriteLine("the sum is :" + (a - b));
            Console.WriteLine("the sum is :" + (a / b));
            Console.WriteLine("the sum is :" + (a * b));
            Console.ReadKey();
        }
    }
}