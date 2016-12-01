using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment_3
{
    class Storage
    {
        private Queue<FoodItem> storagequeue;
        private int totalNumberOfItems, currentNumberOfItems;
        private float totalWeigth;
        private float totalVolume;
        private Mutex myMutex = new Mutex();
        private static Semaphore writeSemaphore, readSemphore;
        private ProgressBar progressBar;


        public Storage(ProgressBar pb)
        {
            totalNumberOfItems = 50;
            currentNumberOfItems = 0;
            totalWeigth = 100;
            totalVolume = 150;


            storagequeue = new Queue<FoodItem>();
            myMutex = new Mutex();
            readSemphore = new Semaphore(0, (int)totalNumberOfItems);
            writeSemaphore = new Semaphore((int)totalNumberOfItems, (int)totalNumberOfItems);

            progressBar = pb;

        }

        public void AddToStorage(FoodItem Food, Label producerLabel)
        {     
            producerLabel.Invoke(new Action(delegate () { producerLabel.Text = "Status: Waiting"; }));
            writeSemaphore.WaitOne();    
            myMutex.WaitOne(100);
            producerLabel.Invoke(new Action(delegate () { producerLabel.Text = "Status: Producing"; }));         
            storagequeue.Enqueue(Food);

            ++currentNumberOfItems;
            totalWeigth += Food.GetWeight;
            totalVolume += Food.GetVolume;
            progressBar.Invoke(new Action(delegate () { progressBar.Increment(1); }));

            readSemphore.Release();
            myMutex.ReleaseMutex();
            Thread.Sleep(100);
        }

        public FoodItem RemoveFromStorage()
        {
           
            readSemphore.WaitOne();
            myMutex.WaitOne(100);
            FoodItem itemToDequeue = storagequeue.Dequeue();

            --currentNumberOfItems;
            totalWeigth -= itemToDequeue.GetWeight;
            totalVolume -= itemToDequeue.GetVolume;
            progressBar.Invoke(new Action(delegate () { progressBar.Increment(-1); }));

            writeSemaphore.Release();
            myMutex.ReleaseMutex();
            Thread.Sleep(100);
            return itemToDequeue;
     
        }

        public int StorageCapacity
        {
            get { return totalNumberOfItems; }
        }

        public int StorageStatus
        {
            get
            {
                if (storagequeue.Count() <= 0)
                {
                    return 0;
                }
                else if(currentNumberOfItems == totalNumberOfItems)
                {
                    return  1;
                }
                else
                {
                    return 2;
                }


               
            }
        }




    }
}
