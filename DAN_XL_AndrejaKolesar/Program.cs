using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_XL_AndrejaKolesar
{
    class Program
    {
        public static string[] documentFormat = new string[2] { "A3", "A4" };

        public static void WriteColorsIntoFile()
        {
            string[] colors = new string[10] { "aqua", "black", "orange", "red", "blue", "purple", "green", "white", "yellow", "rose" };
            using (StreamWriter sw = File.CreateText("Paleta.txt"))
            {
                foreach(string c in colors)
                {
                    sw.WriteLine(c);
                }
            }
        }

        static void Main(string[] args)
        {
            WriteColorsIntoFile();
            Console.ReadKey();
        }
    }
}
