using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment_3
{
    class Factory
    {
        private int count;
        private int from, to;
        private bool produce;
        private FoodItem[] foodbuffer;
        private Random random = new Random();
        private Storage mainStorge;

        public Factory(Storage storage, int count, int from, int to)
        {
            mainStorge = storage;
            this.count = count;
            this.from = from;
            this.to = to;
            foodbuffer = new FoodItem[count];
            FoodItems();
        }

        public void Produce()
        {

            if (mainStorge.IsStorageFull == false && ShouldProduce == true)
            {
                int temp = random.Next(from, to);
                mainStorge.AddToStorage(ProducedFood(temp));
            }

            else if (mainStorge.IsStorageFull == true && ShouldProduce == false)
            {

            }

        }

        public bool ShouldProduce
        {
            get { return produce; }
            set
            {
                produce = value;
            }
        }

        public FoodItem ProducedFood(int FoodBufferPosition)
        {
            return foodbuffer[FoodBufferPosition];
        }

        private void FoodItems()
        {
            //Ändra vikt och volym
            foodbuffer[0] = new FoodItem("Mjök", 1, 1.5f);
            foodbuffer[1] = new FoodItem("Fil", 1, 1f);
            foodbuffer[2] = new FoodItem("Grädde", 0.3f, 0.5f);
            foodbuffer[3] = new FoodItem("HavreGryn", 1, 3f);
            foodbuffer[4] = new FoodItem("Vetekli", 2, 3f);
            foodbuffer[5] = new FoodItem("Vetemjöl", 2, 3f);
            foodbuffer[6] = new FoodItem("Skinka", 1, 1.5f);
            foodbuffer[7] = new FoodItem("Köttbullar", 1, 1.5f);
            foodbuffer[8] = new FoodItem("Prinskorv", 1, 1.5f);
            foodbuffer[9] = new FoodItem("Falukorv", 1, 1.5f);

        }

    }
}
