using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAN_XL_AndrejaKolesar
{
    class Computer
    {
        private object locker1 = new object();
        private object locker2 = new object();
        private object locker = new object();
        AutoResetEvent autoReset = new AutoResetEvent(false);
        public static CountdownEvent countdown = new CountdownEvent(10);
        public static List<int> PrintedDocuments = new List<int>();

        public void CreateThreadForEachComputer()
        {
            Thread[] computers = new Thread[10];
            for (int i = 0; i < 10; i++)
            {
                computers[i] = new Thread(() => SendRequestToPrinter(new Document()))
                {
                    Name = string.Format("Computer_{0}", i + 1)
                };
            }

            foreach (Thread t in computers)
            {
                t.Start();
            }

            while (countdown.CurrentCount > 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (!computers[i].IsAlive)
                    {
                        string name = computers[i].Name;
                        computers[i] = new Thread(() => SendRequestToPrinter(new Document()));
                        computers[i].Name = name;
                        computers[i].Start();
                    }
                }
            }
        }


        public void SendRequestToPrinter(Document document)
        {
            Printer printer = new Printer();
            string computerName = Thread.CurrentThread.Name;
            int computerNumber = Convert.ToInt16(computerName.Split('_')[1]);
            Console.WriteLine("{0} has sent request for printing. Document description - FORMAT:{1}  COLOR:{2}   ORIENTATION:{3}", computerName, document.Format, document.Color, document.Orientation);
            
            if (document.Format == "A3")
            {
                lock (locker1)
                {
                    if (countdown.CurrentCount > 0)
                    {
                        printer.PrintDocument();
                        Console.WriteLine("User of {0} can take {1} document", computerName, document.Format);
                    }
                }
            }
            else
            {
                lock (locker2)
                {
                    if (countdown.CurrentCount > 0)
                    {
                        printer.PrintDocument();
                        Console.WriteLine("User of {0} can take {1} document", computerName, document.Format);
                    }
                }
            }

            lock (locker)
            {
                if (!PrintedDocuments.Contains(computerNumber))
                {
                    countdown.Signal();
                }
                PrintedDocuments.Add(computerNumber);
            }
            Thread.Sleep(100);
        }

    }
}
