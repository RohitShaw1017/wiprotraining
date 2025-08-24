// See https://aka.ms/new-console-template for more information
using System; // using is a keyword to import the namespaces (package)

class Program
{

    // method is in pascal case (eg : AddOperation())
    static void Main()
    {
        // int noOfStudent = 5;
        // string[] sName = new string[noOfStudent];

        // Console.WriteLine("Enter the names of 5 students :");

        // for (int i = 0; i < noOfStudent; i++)
        // {

        //     Console.WriteLine("Enter name of student " + (i + 1));
        //     sName[i] = Console.ReadLine();
        // }
        // Console.WriteLine("Student Data :");
        // for (int i = 0; i < 5; i++)
        // {
        //     Console.WriteLine("The " + (i + 1) + "student name is : " + sName[i]);

        // }
        //     int noOfStudent = 5;

        //  string[] sName = new string[noOfStudent];

        //     Console.WriteLine("Enter the names of 5 students :");

        //     for (int i = 0; i < noOfStudent; i++)
        //     {

        //         Console.WriteLine("Enter name of student " + (i + 1));
        //         sName[i]=Console.ReadLine();
        //     }
        //      Console.WriteLine("Student Data :");
        //         for (int i = 0; i < 5; i++)
        //     {
        //         Console.WriteLine("The " + (i+1) + "student name is : " + sName[i]);
        {
            string[] studentNames = new string[5];
            string[][] studentSubjects = new string[5][]; // Jagged array

            for (int i = 0; i < 5; i++)
            {
                Console.Write($"Enter name of student {i + 1}: ");
                studentNames[i] = Console.ReadLine();

                Console.Write($"How many subjects has {studentNames[i]} taken? ");
                int subCount = int.Parse(Console.ReadLine());

                studentSubjects[i] = new string[subCount]; // Allocate space for this student's subjects

                for (int j = 0; j < subCount; j++)
                {
                    Console.Write($"Enter subject {j + 1} for {studentNames[i]}: ");
                    studentSubjects[i][j] = Console.ReadLine();
                }

                Console.WriteLine();
            }

            // Display
            Console.WriteLine("\n----- Student and Subject Details -----");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Student {i + 1}: {studentNames[i]}");
                Console.WriteLine("Subjects:");
                for (int j = 0; j < studentSubjects[i].Length; j++)
                {
                    Console.WriteLine($" - {studentSubjects[i][j]}");
                }
                Console.WriteLine();
            }

        }

    }
}

