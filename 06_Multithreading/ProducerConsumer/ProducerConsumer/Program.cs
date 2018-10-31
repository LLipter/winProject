using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    class Program
    {
        private static Semaphore empty;
        private static Semaphore full;
        private static Mutex mutex;
        private static Queue<int> buffer;
        private static int product_no;
        private static int current_no;
        private static bool all_produced;

        static void Producer()
        {
            while (true)
            {
                empty.WaitOne();
                mutex.WaitOne();
                if (current_no < product_no)
                {
                    current_no++;
                    buffer.Enqueue(current_no);
                    Console.WriteLine(String.Format("product {0} is produced by {1}", current_no, Thread.CurrentThread.Name));
                }
                else
                {
                    if (!all_produced)
                    {
                        all_produced = true;
                        full.Release();
                    }
                    Console.WriteLine(String.Format("{0} quits", Thread.CurrentThread.Name));
                    mutex.ReleaseMutex();
                    empty.Release();
                    break;
                }
                mutex.ReleaseMutex();
                full.Release();
            }
        }

        static void Consumer()
        {
            while (true)
            {
                full.WaitOne();
                mutex.WaitOne();
                if (all_produced && buffer.Count == 0)
                {
                    Console.WriteLine(String.Format("                                      {0} quits", Thread.CurrentThread.Name));
                    mutex.ReleaseMutex();
                    full.Release();
                    break;
                }
                int product = buffer.Dequeue();
                Console.WriteLine(String.Format("                                      product {0} is comsumed by {1}", product, Thread.CurrentThread.Name));
                mutex.ReleaseMutex();
                empty.Release();
            }
        }

        static void ProducerConsumer(int producer_no, int comsumer_no, int buffer_size, int p_no)
        {
            empty = new Semaphore(buffer_size, buffer_size);
            full = new Semaphore(0, buffer_size);
            mutex = new Mutex();
            buffer = new Queue<int>();
            product_no = p_no;
            current_no = 0;
            all_produced = false;

            Thread[] producers = new Thread[producer_no];
            for(int i = 0; i < producer_no; i++)
            {
                producers[i] = new Thread(new ThreadStart(Producer));
                producers[i].Name = String.Format("Producer {0}", i + 1);
                producers[i].Start();
            }

            Thread[] consumers = new Thread[comsumer_no];
            for (int i = 0; i < comsumer_no; i++)
            {
                consumers[i] = new Thread(new ThreadStart(Consumer));
                consumers[i].Name = String.Format("Consumer {0}", i + 1);
                consumers[i].Start();
            }

            for (int i = 0; i < producer_no; i++)
                producers[i].Join();
            for (int i = 0; i < comsumer_no; i++)
                consumers[i].Join();

            Console.WriteLine("End of producer consumer problem");
        }

        static void Main(string[] args)
        {
            // ProducerConsumer(1, 1, 10, 20);
            // ProducerConsumer(1, 5, 10, 20);
            // ProducerConsumer(5, 1, 10, 20);
            ProducerConsumer(5, 5, 10, 50);
        }

        
    }
}
