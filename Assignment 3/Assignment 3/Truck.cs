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
        int currentNrGoods;
        float currentNrWeight, currentNrVolume;

        int totalGoods;
        float totalWeight;
        float totalVolume;

        Storage storage;
        FoodItem[] foodInTruck;
        Random rand = new Random();

        Label totalGoodLabel, totalWeigthLabel, totalVolumeLabel, statusLabel;
        ListBox loadedGoods;
        bool loadTruck;

        private delegate void DisplayDelegate(string s, ListBox listBox);

        public Truck(Storage storage, int totalGoods, float totalWeight, float totalVolume,
            Label l1, Label l2, Label l3, Label status, ListBox lb)
        {
            this.storage = storage;
            this.totalGoods = totalGoods;
            foodInTruck = new FoodItem[totalGoods];

            currentNrGoods = 0;
            currentNrWeight = 0;
            currentNrVolume = 0;

            totalGoodLabel = l1;
            loadedGoods = lb;
            loadTruck = false;

            totalWeigthLabel = l2;
            totalVolumeLabel = l3;
            statusLabel = status;
            this.totalWeight = totalWeight;
            this.totalVolume = totalVolume;
        }

        /// <summary>
        /// Lägger till objekt från storage till truck. 
        /// Kollar så att inte kön är full och kör sedan en for loop. 
        /// Denna avbryts om maximal vikt, volym eller antal fooditems på trucken överskrider eller om 
        /// storage är tomt. 
        /// Tråden sätts då i Wait.
        /// </summary>
        public void LoadTruck()
        {
            if (LoadingTruck == true)
            {
                for (int i = 0 + currentNrGoods; i < totalGoods; i++)
                {

                    foodInTruck[i] = FoodFromStorage();

                    if (currentNrWeight + foodInTruck[i].GetWeight > totalWeight || currentNrVolume + foodInTruck[i].GetVolume > totalVolume)
                    {
                        break;
                    }

                    statusLabel.Invoke(new Action(delegate () { statusLabel.Text = "Status: Loading"; }));
                    ++currentNrGoods;
                    currentNrWeight += foodInTruck[i].GetWeight;
                    currentNrVolume += foodInTruck[i].GetVolume;

                    totalGoodLabel.Invoke(new Action(delegate () { totalGoodLabel.Text = currentNrGoods.ToString(); }));
                    totalWeigthLabel.Invoke(new Action(delegate () { totalWeigthLabel.Text = currentNrWeight.ToString(); }));
                    totalVolumeLabel.Invoke(new Action(delegate () { totalVolumeLabel.Text = currentNrVolume.ToString(); }));

                    loadedGoods.Invoke(new DisplayDelegate(DisplayString), new object[] { foodInTruck[i].GetName, loadedGoods });
                    Thread.Sleep(rand.Next(100, 1000));

                }
                statusLabel.Invoke(new Action(delegate () { statusLabel.Text = "Status: Unloading"; }));

                LoadOff();
            }
   
            else
            {
                Wait();
            }
        }

        /// <summary>
        /// Sätter tråden i vänt läge tills boolen ändras
        /// </summary>
        private void Wait()
        {
            statusLabel.Invoke(new Action(delegate () { statusLabel.Text = "Status: Waiting"; }));

            while (LoadingTruck == false) { Thread.Sleep(rand.Next(100, 400)); }
            LoadTruck();

        }

        /// <summary>
        /// metod som kallar på metod i Storage
        /// </summary>
        /// <returns></returns>
        public FoodItem FoodFromStorage()
        {
            return storage.RemoveFromStorage();
        }

        /// <summary>
        /// Rensar listboxes och ställer om alla current värden till 0.
        /// tråden sätts i sleep sen körs loadtruck igen.
        /// </summary>
        public void LoadOff()
        {

            foodInTruck = new FoodItem[totalGoods];
            loadedGoods.Invoke(new Action(delegate () { loadedGoods.Items.Clear(); }));
            currentNrGoods = 0;
            currentNrWeight = 0;
            currentNrVolume = 0;
            Thread.Sleep(rand.Next(4000,5000));
            LoadTruck();

        }

        /// <summary>
        /// property för boolen loadtruck
        /// </summary>
        public bool LoadingTruck
        {
            get { return loadTruck; }
            set
            {
                loadTruck = value;
            }
        }

        private void DisplayString(string s, ListBox listBox)
        {
            listBox.Items.Add(s);
        }


    }
}
