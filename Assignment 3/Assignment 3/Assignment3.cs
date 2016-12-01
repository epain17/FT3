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
        private Factory Scan, Arla;
        private Truck ICA;
        private Thread ScanThread, ArlaThread;
        private Thread ICAThread;


        public Assignment3()
        {
            InitializeComponent();
            CreateStorageFactorysandTrucks();
            CreateFactoryThreads();
            CreateTruckThreads();
        }

        private void ScanStart_Click(object sender, EventArgs e)
        {
            Scan.ShouldProduce = true;
            
        }

        private void ScanStop_Click(object sender, EventArgs e)
        {
            Scan.ShouldProduce = false;
        }

        private void StartArla_Click(object sender, EventArgs e)
        {
            Arla.ShouldProduce = true;
        }

        private void StopArla_Click(object sender, EventArgs e)
        {
            Arla.ShouldProduce = false;
        }

        private void StartLoadICA_Click(object sender, EventArgs e)
        {
            ICA.LoadingTruck = true;

        }

        private void StopLoadICA_Click(object sender, EventArgs e)
        {
            ICA.LoadingTruck = false;
        }

        private void CreateStorageFactorysandTrucks()
        {
            mainStorage = new Storage(StorageCapacity);
            foodList = new FoodList();
            Scan = new Factory(mainStorage, 6, 9, StatusScan, foodList);
            Arla = new Factory(mainStorage, 0, 2, StatusArla, foodList);

            ICA = new Truck(mainStorage, 10, ItemsICA, IcaListbox);

           
        }

        private void CreateFactoryThreads()
        {
            ScanThread = new Thread(Scan.StopProduction);
            ArlaThread = new Thread(Arla.StopProduction);
            ScanThread.Start();
            ArlaThread.Start();


        }

        private void CreateTruckThreads()
        {
            ICAThread = new Thread(ICA.LoadTruck);
            ICAThread.Start();
        }
    }
}
