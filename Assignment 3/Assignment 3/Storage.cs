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
        private int totalNumberOfItems;
        private float totalWeigth;
        private float totalVolume;
        private Mutex m = new Mutex();

        public Storage(int NumberOfITems, float TotalWeight, float TotalVolume)
        {
            totalNumberOfItems = NumberOfITems;
            totalWeigth = TotalWeight;
            totalVolume = TotalVolume;
        }

        public void AddToStorage(FoodItem Food)
        {
            //Kanske måste lägga till if statements för att 
            //kolla maximala vikten och volumen för lagret
            m.WaitOne();
            if (storagequeue.Count() != totalNumberOfItems)
            {
                totalWeigth += Food.GetWeight;
                totalVolume += Food.GetVolume;
                storagequeue.Enqueue(Food);
            }
            m.ReleaseMutex();
        }

        public FoodItem RemoveFromStorage()
        {
            if (storagequeue.Count() != 0)
            {
                m.WaitOne();
                FoodItem temp = storagequeue.Dequeue();


                totalWeigth -= temp.GetWeight;
                totalVolume -= temp.GetVolume;
                m.ReleaseMutex();
                return temp;
            }
            return null;
        }

        public int StorageCapacity
        {
            get { return totalNumberOfItems; }
        }

        public bool IsStorageFull
        {
            get
            {
                if (storagequeue.Count() != totalNumberOfItems)
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsStorageEmpty
        {
            get
            {
                if (storagequeue.Count() <= 0)
                {
                    return true;
                }
                return false;
            }
        }


    }
}
