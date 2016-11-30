using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3
{
    class Truck
    {
        int totalGoods;
        int count;
        float totalWeight;
        float totalVolume;
        Storage storage;
        bool loadTruck;
        FoodItem[] foodInTruck;

        public Truck(Storage storage, int totalGoods, float totalWeight, float totalVolume)
        {
            this.storage = storage;
            this.totalGoods = totalGoods;
            this.totalWeight = totalWeight;
            this.totalVolume = totalVolume;
            foodInTruck = new FoodItem[totalGoods];
            count = 0;

        }

        public void LoadTruck()
        {
            for (int i = 0 + count; i < totalGoods; i++)
            {
                if (storage.IsStorageEmpty == false)
                {
                    foodInTruck[i] = FoodFromStorage();
                }
                else
                {

                    break;
                }

            }

        }

        public FoodItem FoodFromStorage()
        {
            return storage.RemoveFromStorage();
        }

        public void LoadOff()
        {
            foodInTruck = new FoodItem[totalGoods];
        }




    }
}
