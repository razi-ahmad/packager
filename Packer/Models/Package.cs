using System;

namespace Packer.Models
{
    public class Package 
    {

        public Package(int index, double weight, int price)
        {
            Index = index;
            Weight = weight;
            Price = price;
        }
        public int Index { get; set; }
        public double Weight { get; set; }
        public int Price { get; set; }
       
    }
}
