using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAN_XL_AndrejaKolesar
{
    class Printer
    {
        public void PrintDocument()
        {
            Thread.Sleep(1000);
            Console.Write("Document has been successfully printed.");
        }
    }
}
