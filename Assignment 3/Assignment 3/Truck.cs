using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment_3
{
    class Truck
    {
        int currentNrGoods, currentNrWeight, currentNrVolume;
        int totalGoods;
        //float totalWeight;
        //float totalVolume;
        Storage storage;
        bool loadTruck;
        FoodItem[] foodInTruck;
        Random rand = new Random();
        Label totalGoodLabel/*, totalWeigthLabel, totalVolumeLabel*/;
        ListBox loadedGoods;

        private delegate void DisplayDelegate(string s, ListBox listBox);

        public Truck(Storage storage, int totalGoods, /*float totalWeight, float totalVolume,*/
            Label l1/*, Label l2, Label l3*/, ListBox lb)
        {
            this.storage = storage;
            this.totalGoods = totalGoods;
            foodInTruck = new FoodItem[totalGoods];

            currentNrGoods = 0;
            currentNrWeight = 0;
            currentNrVolume = 0;

            totalGoodLabel = l1;
            loadedGoods = lb;

            

            //l2 = totalWeigthLabel;
            //l3 = totalVolumeLabel;
            //this.totalWeight = totalWeight;
            //this.totalVolume = totalVolume;
        }

        public void LoadTruck()
        {
            if (storage.StorageStatus != 0 && LoadingTruck == true)
            {
                for (int i = 0 + currentNrGoods; i < totalGoods; i++)
                {
                    foodInTruck[i] = FoodFromStorage();
                    ++currentNrGoods;
                    totalGoodLabel.Invoke(new Action(delegate () { totalGoodLabel.Text = currentNrGoods.ToString(); }));
                    loadedGoods.Invoke(new DisplayDelegate(DisplayString), new object[] { foodInTruck[i].GetName, loadedGoods });
                  
                }
                LoadOff();

            }
            else
            {
                while(LoadingTruck == false)
                {
                    if(LoadingTruck == true)
                    {
                        LoadTruck();
                    }
                }
            }
        }

        private void DisplayString(string s, ListBox listBox)
        {
            listBox.Items.Add(s);
        }

        public FoodItem FoodFromStorage()
        {
            return storage.RemoveFromStorage();
        }

        public void LoadOff()
        {

            foodInTruck = new FoodItem[totalGoods];
            loadedGoods.Invoke(new Action(delegate () { loadedGoods.Items.Clear(); }));
            currentNrGoods = 0;
            Thread.Sleep(rand.Next(100, 2000));
            LoadTruck();

        }

        public bool LoadingTruck
        {
            get { return loadTruck; }
            set
            {
                loadTruck = value;
            }
        }



    }
}
