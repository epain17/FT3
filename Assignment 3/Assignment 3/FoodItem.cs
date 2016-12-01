using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3
{
    class FoodItem
    {
        private string name;
        private float weight;
        private float volume;

        public FoodItem(string Name, float Weight, float Volume)
        {
            name = Name;
            weight = Weight;
            volume = Volume;
        }

        public float GetWeight
        {
            get { return weight; }
        }

        public float GetVolume
        {
            get { return volume; }
        }

        public string GetName
        {
            get { return name; }
        }
    }
}
