// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.Linq;

class StudentMarks
{
    static void Main()
    {
        // Dictionary to hold student name as key and list of subject marks as value
        Dictionary<string, List<int>> students = new Dictionary<string, List<int>>();

        // Adding sample students and their subject marks
        students.Add("Rohit", new List<int> { 80, 90, 85 });
        students.Add("ansh", new List<int> { 75, 88, 92 });
        students.Add("Sakshi", new List<int> { 60, 70, 65 });

        Console.WriteLine("Student Details with Average Scores:");

        // Loop through each student
        foreach (var student in students)
        {
            string name = student.Key;
            List<int> marks = student.Value;

            // Calculate average using LINQ

            double average = marks.Average();
            

            Console.WriteLine($"Name: {name}");
            Console.WriteLine("Marks: " + string.Join(", ", marks));
            Console.WriteLine($"Average Score: {average:F2}");
        }
    }
}
