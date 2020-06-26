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
            Thread.Sleep(1000);
            int computerNumber = Convert.ToInt16(Thread.CurrentThread.Name.Split('_')[1]);
            Console.WriteLine("<-- Document has been successfully printed. User of {0} can take {1} document", Thread.CurrentThread.Name, document.Format);
            //locking countdown signal
            lock (locker)
            {
                if (!Computer.PrintedDocuments.Contains(computerNumber))
                {
                    Computer.countdown.Signal();
                }
                Computer.PrintedDocuments.Add(computerNumber);
            }
        }
    }
}
