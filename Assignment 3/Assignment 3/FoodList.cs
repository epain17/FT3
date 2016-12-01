using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3
{
    class FoodList
    {
        FoodItem[] foodList;

        public FoodList()
        {
            foodList = new FoodItem[20];
            FoodItems();
        }

        public void FoodItems()
        {
            foodList[0] = new FoodItem("Mjök", 1, 1.5f);
            foodList[1] = new FoodItem("Fil", 1, 1f);
            foodList[2] = new FoodItem("Grädde", 0.3f, 0.5f);
            foodList[3] = new FoodItem("HavreGryn", 1, 3f);
            foodList[4] = new FoodItem("Vetekli", 2, 3f);
            foodList[5] = new FoodItem("Vetemjöl", 2, 3f);
            foodList[6] = new FoodItem("Skinka", 1, 1.5f);
            foodList[7] = new FoodItem("Köttbullar", 1, 1.5f);
            foodList[8] = new FoodItem("Prinskorv", 1, 1.5f);
            foodList[9] = new FoodItem("Falukorv", 1, 1.5f);

        }

        public FoodItem Food(int i)
        {
            return foodList[i];
        }
    }
}
