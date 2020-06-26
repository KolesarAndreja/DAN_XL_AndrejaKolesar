using System;
using System.Collections.Generic;
using System.Threading;

namespace DAN_XL_AndrejaKolesar
{
    class Computer
    {
        private object locker1 = new object();
        private object locker2 = new object();

        //decrement when computer for the first time prints document
        public static CountdownEvent countdown = new CountdownEvent(10);

        //list of computers numbers that had printed documents. If computer X printed 2 documents, we will have value X twice in this list
        public static List<int> PrintedDocuments = new List<int>();

        /// <summary>
        /// Method creates and starts threads for every computer. Each computer will send request to printer for specific Document.
        /// </summary>
        public void CreateAndStartEachComputer()
        {
            Thread[] computers = new Thread[10];
            for (int i = 0; i < 10; i++)
            {
                computers[i] = new Thread(() => SendRequestToPrinter(new Document()))
                {
                    Name = string.Format("Computer_{0}", i + 1)
                };
                computers[i].Start();
            }

            //if computer printed once some document, it can print again some other document until each computer prints one document
            while (countdown.CurrentCount > 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    //when thread is not alive, make new one with same name as old one
                    if (!computers[i].IsAlive)
                    {
                        string name = computers[i].Name;
                        //send request for new Document
                        computers[i] = new Thread(() => SendRequestToPrinter(new Document()));
                        computers[i].Name = name;
                        computers[i].Start();
                    }
                }
            }
            //Console.WriteLine("\n*Printing is completed*");
        }

        /// <summary>
        /// Method sends request to printer and display on console this request, then selected Printer prints this document.
        /// </summary>
        /// <param name="document"></param>
        public void SendRequestToPrinter(Document document)
        {
            Printer printer = new Printer();
            string computerName = Thread.CurrentThread.Name;
            Console.WriteLine("--> {0} has sent request for printing. Document description - FORMAT:{1}  COLOR:{2}   ORIENTATION:{3}", computerName, document.Format, document.Color, document.Orientation);
            
            if (document.Format == "A3")
            {
                //only one document can be printed in same time on printer1
                lock (locker1)
                {
                    if (countdown.CurrentCount > 0)
                    {
                        printer.PrintDocument(document);
                    }
                }
            }
            else
            {
                //only one document can be printed in same time on printer2
                lock (locker2)
                {
                    if (countdown.CurrentCount > 0)
                    {
                        printer.PrintDocument(document);
                    }
                }
            }
        }

    }
}
