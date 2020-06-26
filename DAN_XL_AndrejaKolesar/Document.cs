using System;
using System.IO;
using System.Linq;

namespace DAN_XL_AndrejaKolesar
{
    class Document
    {
        public Random random = new Random();
        //get random format from array
        public string Format
        {
            get
            {
                return Program.documentFormat[random.Next(0, 2)];
                
            }
        }

        //get random color from file
        public string Color
        {
            get
            {
                //skip random lines
                int n = random.Next(0, 10);
                string line = File.ReadLines("Paleta.txt").Skip(n).Take(1).First();
                return line;
            }
        }
        //get random orientation
        public string Orientation {
            get
            {
                int n = random.Next(0, 2);
                if(n == 0)
                {
                    return "portrait";
                }
                else
                {
                    return "landscape";
                }
            }
        }

    }
}
