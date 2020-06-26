using System;
using System.Threading;

namespace DAN_XL_AndrejaKolesar
{
    class Printer
    {

        private object locker = new object();

        /// <summary>
        /// Print document
        /// </summary>
        public void PrintDocument(Document document)
        {
            //printing duration is 1000ms
            string computerName = Thread.CurrentThread.Name;
            Thread.Sleep(1000);
            Console.WriteLine("<-- Document has been successfully printed. User of {0} can take {1} document", computerName, document.Format);
            //locking countdown signal
            lock (locker)
            {
                if (!Computer.ComputersThatPrintedDocuments.Contains(computerName))
                {
                    Computer.countdown.Signal();
                }
                Computer.ComputersThatPrintedDocuments.Add(computerName);
                
            }
        }
    }
}
