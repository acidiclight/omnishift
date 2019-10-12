using System;

namespace BitPhoenix.OmniShift
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("OmniShift 0.0.1");
            Console.WriteLine("Licensed under MIT, copyright (c) 2019 Michael VanOverbeek.");
            Console.WriteLine();
            Console.WriteLine();

            string path = "";

            if (args.Length > 0)
                path = string.Join(' ', args);

            if(string.IsNullOrWhiteSpace(path))
            {
                do
                {
                    Console.Write("Enter path to .skn file: ");
                    path = Console.ReadLine();
                } while (string.IsNullOrWhiteSpace(path));
            }

            try
            {
                var skinData = OmniShift.LoadSkin(path);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
#if DEBUG
                throw ex;
#endif
            }
        }
    }
}
