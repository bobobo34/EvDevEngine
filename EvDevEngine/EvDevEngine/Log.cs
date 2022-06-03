using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvDevEngine.EvDevEngine
{
    public class Log
    {
        public static void Normal(object msg)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"[MSG]- {msg}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void Info(object msg)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[INFO]- {msg}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void Warning(object msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[WARNING]- {msg}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void Error(object msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR]- {msg}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
