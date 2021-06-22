using System;
using System.Collections.Generic;
using System.Text;

namespace Packer.Models
{
    public class PackageLineItem
    {
        public PackageLineItem()
        {
            Packages = new List<Package>();
        }

        public PackageLineItem(int maxWeight, List<Package> packages)
        {
            MaxWeight = maxWeight;
            Packages = packages;
        }
        public int MaxWeight { get; set; }
        public List<Package> Packages { get; set; }
    }
}
