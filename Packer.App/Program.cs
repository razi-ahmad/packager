using System;

namespace Packer.App
{
    class Program
    {
        static void Main(string[] args)
        {
            
            do
            {
                Console.Write("Enter file path :");
                string filePath = Console.ReadLine();

                Console.WriteLine("Result");
                Console.WriteLine("------------------------------------------------------------------");

                try
                {
                    var result = Packer.Pack(filePath);
                    Console.Write(result);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error : {e.Message}");
                }

                Console.WriteLine("-------------------------------------------------------------------");
                Console.Write("Try Again (Y/N) ? :");

                string choice = Console.ReadLine();
                if (choice.Equals("N", StringComparison.OrdinalIgnoreCase)) break;                    

            } while (true);


        }


    }
}
