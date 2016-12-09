using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment_3
{
    class Factory
    {
        private int from, to;
        private bool produce;
        private Random random = new Random();
        private Storage mainStorge;
        Label producerLabel;
        FoodList foodList;

        public Factory(Storage storage, int from, int to, Label pL, FoodList foodList)
        {
            mainStorge = storage;
            this.from = from;
            this.to = to;
            producerLabel = pL;
            this.foodList = foodList;
            produce = false;

        }

        /// <summary>
        /// producerar och lägger till fooditems i Storage
        /// </summary>
        public void Produce()
        {
            Console.WriteLine(Thread.CurrentThread.Name + " Started");

            while (ShouldProduce == true)
            {
                int temp = random.Next(from, to);
                mainStorge.AddToStorage(ProducedFood(temp), producerLabel);
                Thread.Sleep(random.Next(100, 1000));
            }
            StopProduction(producerLabel);
        }

        /// <summary>
        /// stopper produktion sålänge shouldproduce är false
        /// </summary>
        /// <param name="label"></param>
        public void StopProduction(Label label)
        {
            Console.WriteLine(Thread.CurrentThread.Name + " Stopped");


            while (ShouldProduce == false) { Thread.Sleep(random.Next(100, 400)); }
            Produce();


        }

        /// <summary>
        /// hämtar ett fooditem från foodlist
        /// </summary>
        /// <param name="FoodBufferPosition"></param>
        /// <returns></returns>
        public FoodItem ProducedFood(int FoodBufferPosition)
        {
            return foodList.Food(FoodBufferPosition);
        }

        /// <summary>
        /// ändrar boolen produce
        /// </summary>
        public bool ShouldProduce
        {
            get { return produce; }
            set
            {
                produce = value;
            }
        }



    }
}
