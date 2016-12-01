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
        }

        public void Produce()
        {
           while (ShouldProduce == true)
            {
                int temp = random.Next(from, to);
                mainStorge.AddToStorage(ProducedFood(temp), producerLabel);
                if(ShouldProduce == false)
                {
                    StopProduction();
                }
            }

        }

        public void StopProduction()
        {
            while(ShouldProduce == false)
            {
                if(ShouldProduce == true)
                {
                    Produce();
                }
            }
        }

        public FoodItem ProducedFood(int FoodBufferPosition)
        {
            return foodList.Food(FoodBufferPosition);
        }

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
