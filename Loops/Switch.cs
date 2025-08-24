using System;
public class Loops
{
    static void Main(string[] args)
    {
        Console.WriteLine("enter the number: ");
        int num = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("enter the option:");
        int choice = Convert.ToInt32(Console.ReadLine());
        switch (choice)
        {
            case 1:
                Console.WriteLine("checking if the number is even or not:");
                if (num % 2 == 0)
                {
                    Console.WriteLine("even");
                }
                else
                {
                    Console.WriteLine("not even");
                }
                break;
        }
    }
}