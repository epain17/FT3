using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment_3
{
    public partial class Assignment3 : Form
    {
        private Storage mainStorage;
        private FoodList foodList;
        private Factory Scan, Arla, AxaFood;
        private Truck ICA, Coop, CityGross;
        private Thread ScanThread, ArlaThread, AxaFoodThread;
        private Thread ICAThread, CoopThread, CityGrossThread;


        public Assignment3()
        {
            InitializeComponent();
            StartDefaults();
            CreateStorageFactorysandTrucks();
            CreateFactoryThreads();
            CreateTruckThreads();
        }

        //Factory Buttons
        private void ScanStart_Click(object sender, EventArgs e)
        {
            Scan.ShouldProduce = true;
            ScanStop.Enabled = true;
            ScanStart.Enabled = false;

            
        }

        private void ScanStop_Click(object sender, EventArgs e)
        {
            Scan.ShouldProduce = false;
            ScanStop.Enabled = false;
            ScanStart.Enabled = true;

        }

        private void StartArla_Click(object sender, EventArgs e)
        {
            Arla.ShouldProduce = true;
            StopArla.Enabled = true;
            StartArla.Enabled = false;
        }

        private void StopArla_Click(object sender, EventArgs e)
        {
            Arla.ShouldProduce = false;
            StopArla.Enabled = false;
            StartArla.Enabled = true;
        }

        private void StartAxFood_Click(object sender, EventArgs e)
        {
            AxaFood.ShouldProduce = true;
            StartAxFood.Enabled = false;
            StopAxfood.Enabled = true;
        }

        private void StopAxfood_Click(object sender, EventArgs e)
        {
            AxaFood.ShouldProduce = false;
            StartAxFood.Enabled = true;
            StopAxfood.Enabled = false;
        }


        //Trucks Buttons
        private void StartLoadICA_Click(object sender, EventArgs e)
        {
            ICA.LoadingTruck = true;
            StartLoadICA.Enabled = false;
            StopLoadICA.Enabled = true;

        }

        private void StopLoadICA_Click(object sender, EventArgs e)
        {
            ICA.LoadingTruck = false;
            StopLoadICA.Enabled = false;
            StartLoadICA.Enabled = true;
        }

        private void CoopStartLoad_Click(object sender, EventArgs e)
        {
            Coop.LoadingTruck = true;
            CoopStartLoad.Enabled = false;
            CoopStopLoad.Enabled = true;
        }

        private void CoopStopLoad_Click(object sender, EventArgs e)
        {
            Coop.LoadingTruck = false;
            CoopStartLoad.Enabled = true;
            CoopStopLoad.Enabled = false;
        }

        private void CGStartLoad_Click(object sender, EventArgs e)
        {
            CityGross.LoadingTruck = true;
            CGStartLoad.Enabled = false;
            CGStop.Enabled = true;
        }

        private void CGStop_Click(object sender, EventArgs e)
        {
            CityGross.LoadingTruck = false;
            CGStartLoad.Enabled = true;
            CGStop.Enabled = false;
        }




        private void CreateStorageFactorysandTrucks()
        {
            mainStorage = new Storage(StorageCapacity);
            foodList = new FoodList();

            Scan = new Factory(mainStorage, 6, 9, StatusScan, foodList);
            Arla = new Factory(mainStorage, 0, 2, StatusArla, foodList);
            AxaFood = new Factory(mainStorage, 3, 5, StatusAxFood, foodList);
            

            ICA = new Truck(mainStorage, 10, 12, 15, ItemsICA, WeightICA, IcaVolume, IcaStatus, IcaListbox);
            Coop = new Truck(mainStorage, 12, 5, 20, CoopUnits, CoopWeight, VolumeUnitCoop, CoopStatus, CoopListbox);
            CityGross = new Truck(mainStorage, 5, 20, 4, CGUnitItem, CGunitWeight, CGUnitVolume, CgStatus, CityGrossListbox);

           
        }

        private void CreateFactoryThreads()
        {
            ScanThread = new Thread(Scan.Produce);
            ArlaThread = new Thread(Arla.Produce);
            AxaFoodThread = new Thread(AxaFood.Produce);
            ScanThread.Start();
            ArlaThread.Start();
            AxaFoodThread.Start();
        }

        private void CreateTruckThreads()
        {
            ICAThread = new Thread(ICA.LoadTruck);
            CoopThread = new Thread(Coop.LoadTruck);
            CityGrossThread = new Thread(CityGross.LoadTruck);
            ICAThread.Start();
            CoopThread.Start();
            CityGrossThread.Start();          
        }

        private void StartDefaults()
        {
            ScanStop.Enabled = false;
            StopArla.Enabled = false;
            StopAxfood.Enabled = false;

            StopLoadICA.Enabled = false;
            CoopStopLoad.Enabled = false;
            CGStop.Enabled = false;

        }

    }
}
