using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment_3
{
    class Storage
    {
        private Queue<FoodItem> storagequeue = new Queue<FoodItem>();
        private int numberOfItems;
        private float totalWeigth;
        private float totalVolume;
        private Mutex m = new Mutex();

        public Storage(int NumberOfITems, float TotalWeight, float TotalVolume)
        {
            numberOfItems = NumberOfITems;
            totalWeigth = TotalWeight;
            totalVolume = TotalVolume;
        }

        public void AddToStorage(FoodItem Food)
        {
            //Kanske måste lägga till if statements för att 
            //kolla maximala vikten och volumen för lagret
            m.WaitOne();
            if (storagequeue.Count() != numberOfItems)
            {
                totalWeigth += Food.GetWeight;
                totalVolume += Food.GetVolume;
                storagequeue.Enqueue(Food);
            }
            m.ReleaseMutex();
        }

        public void RemoveFromStorage(FoodItem Food)
        {
            m.WaitOne();
            if(storagequeue.Count() != 0)
            {
                totalWeigth -= Food.GetWeight;
                totalVolume -= Food.GetVolume;
                storagequeue.Dequeue();
            }
            m.ReleaseMutex();
        }

        public int StorageCapacity
        {
            get { return numberOfItems; }
        }


    }
}
