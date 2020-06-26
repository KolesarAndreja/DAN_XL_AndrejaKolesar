using System;
using System.IO;

namespace DAN_XL_AndrejaKolesar
{
    class Program
    {
        /// <summary>
        /// Print into file colors from array
        /// </summary>
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
            //write colors in Paleta.txt
            WriteColorsIntoFile();
            Computer c = new Computer();
            c.CreateAndStartEachComputer();
            Console.ReadKey();
        }
    }
}
