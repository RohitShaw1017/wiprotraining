// See https://aka.ms/new-console-template for more information
using System;
class Loops
{
    static void Main(string[] args)
    {
        Console.WriteLine("enter the number: ");
        int num = Convert.ToInt32(Console.ReadLine());
        if (num % 2 == 0)
        {
            Console.WriteLine("even ");
        }
        else
        {
            Console.WriteLine("odd ");
        }
    }
}
