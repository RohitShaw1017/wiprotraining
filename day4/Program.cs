// See https://aka.ms/new-console-template for more inf
class StringPrograms
{
    static void Main()
    {

        string text = "CSharp Language invented in 2002";

        int length = text.Length;
        Console.WriteLine("The Length of a string : " + length);
        // Console.WriteLine(text.IndexOf(" "));
        int count = 1;
        int space = 0;
        for (int i = 0; i < text.Length; i++)
        {
            text = text.Substring(i, count);
            count++;
            if (text == " ")
            {
                space++;

            }
            Console.WriteLine(space);

        }

            Console.WriteLine(space);

    }
}
