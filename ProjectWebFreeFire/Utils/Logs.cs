using System;
using System.Threading;

namespace ProjectWebFreeFire.Utils
{
    public class Logs
    {
        public static void WriteBlue(string txt)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(txt);
            Console.ResetColor();
        }
        public static void WriteYellow(string txt)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(txt + "\n");
            Console.ResetColor();
            Thread.Sleep(4000);
        }
        public static void WriteYellow(object txt)
        {
            Console.ForegroundColor = ConsoleColor.Yellow; 
            Console.WriteLine(txt);
            Console.ResetColor();
        }
        public static void WriteRed(string txt)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(txt);
            Console.ResetColor();
        }
        public static void WriteCyan(string txt)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(txt);
            Console.ResetColor();
        }
    }
}
