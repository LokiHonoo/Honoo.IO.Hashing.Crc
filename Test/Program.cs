using Honoo.IO.Hashing;
using System;

namespace Test
{
    internal class Program
    {
        #region Main

        private static void Main()
        {
            var a = CrcName.ARC;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=======================================================================================");
                Console.WriteLine();
                Console.WriteLine("                        Honoo.IO.Hashing.Crc   runtime " + Environment.Version);
                Console.WriteLine();
                Console.WriteLine("=======================================================================================");
                //
                Console.WriteLine();
                Console.WriteLine("  1. Compute all");
                Console.WriteLine("  2. Speed");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("Choice a project:");
                while (true)
                {
                    var kc = Console.ReadKey(true).KeyChar;
                    switch (kc)
                    {
                        case '1': Console.Clear(); Test.Test1(); break;
                        case '2': Console.Clear(); Speed.Test(); break;
                        default: continue;
                    }
                    break;
                }
                Console.WriteLine();
                Console.Write("Press any key to Main Menu...");
                Console.ReadKey(true);
            }
        }

        #endregion Main
    }
}