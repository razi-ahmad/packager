using Packer.Extensions;
using Packer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Packer
{
    public class PackageSelector : IPackageSelector
    {
        public string SelectPackages(PackageLineItem packageLineItem)
        {   
            //sorting packages by weight to pick least weight if price is same for two items
            var packages = packageLineItem.Packages.OrderBy(x => x.Weight).ToList();

            //Keep track of original indexes after sorting            
            var originalIndexes = packages.Select(x => x.Index).ToArray();

            //create arrays of weights and prices
            int[] prices = packages.Select(x => x.Price).ToArray();
            int[] weights = packages.Select(x => (int)x.Weight).ToArray();

            int maxWeight = packageLineItem.MaxWeight;
            int packageCount = packages.Count;

            //get indexes of selected packages using knapsack 0/1 algorithm
            var interemIndexes = KnapSack(maxWeight, weights, prices, packageCount);

            //indexes returned above are indexes based on the order of items
            //get the real indexes from the original indexes array 
            var transformedIndexes = TransformIndexes(interemIndexes, originalIndexes);

            //return result 
            return transformedIndexes.AsString();                        
        }

       
        private List<int> KnapSack(int maxWeight, int[] weights, int[] prices, int capacity)
        {
            int currentCapacity, currentWeight;
            int[,] Maxtrix = new int[capacity + 1, maxWeight + 1];

            // Build table Maxtrix[][] in bottom
            // up manner
            for (currentCapacity = 0; currentCapacity <= capacity; currentCapacity++)
            {
                for (currentWeight = 0; currentWeight <= maxWeight; currentWeight++)
                {
                    if (currentCapacity == 0 || currentWeight == 0)
                        Maxtrix[currentCapacity, currentWeight] = 0;

                    else if (weights[currentCapacity - 1] <= currentWeight)
                        Maxtrix[currentCapacity, currentWeight] = Math.Max(prices[currentCapacity - 1] + Maxtrix[currentCapacity - 1, currentWeight - weights[currentCapacity - 1]], Maxtrix[currentCapacity - 1, currentWeight]);
                    else
                        Maxtrix[currentCapacity, currentWeight] = Maxtrix[currentCapacity - 1, currentWeight];
                }
            }

            int result = Maxtrix[capacity, maxWeight];


            // Iterate over matrix to pick included items

            currentWeight = maxWeight;
            var choosenIndexes = new List<int>();

            for (currentCapacity = capacity; currentCapacity > 0 && result > 0; currentCapacity--)
            {                
                if (result == Maxtrix[currentCapacity - 1, currentWeight])
                    continue;
                else
                {
                    // This item is included.                    
                    choosenIndexes.Add(currentCapacity - 1);

                    // Since this weight is included its
                    // value is deducted
                    result = result - prices[currentCapacity - 1];
                    currentWeight = currentWeight - weights[currentCapacity - 1];
                }
            }

            return choosenIndexes;
        }

        private List<int> TransformIndexes(List<int> givenIndexes, int[] originalIndexes)
        {            
            return givenIndexes
                    .Select(index => originalIndexes[index])
                    .ToList();           
        }

    }
}
