using Packer.Exceptions;
using Packer.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Packer.Helpers
{
    public class PackageLineItemParser : IParser
    {
        public List<PackageLineItem> Parse(string filePath)
        {
            //ensure file path is valid 
            EnsureFileIsValid(filePath);

            var packageLineItems = new List<PackageLineItem>();
            using (var file = new StreamReader(filePath))
            {
                string line = string.Empty;
                while ((line = file.ReadLine()) != null)
                {
                    var packageLineItem = ParsePackageLineItem(line);
                    packageLineItems.Add(packageLineItem);
                }
            }

            return packageLineItems;
        }

        private  PackageLineItem ParsePackageLineItem(string line)
        {
            // if line is missing ':' than there is a format issue
            if (!line.Contains(":"))
                throw new ApiException("format of the data is invalid. Expecting ':' after weight value");

            // first value before ':' is weight
            string maxWeightValue = line.Substring(0, line.IndexOf(':')).Trim();
            int maxWeight;
            if (!int.TryParse(maxWeightValue, out maxWeight))
                throw new ApiException("Error parsing file , invalid value for Maximum Weight");

            // part after colon (:) should be the items
            var packages = line.Split(':')[1];
            var charArray = packages.ToCharArray();

            var packageList = new List<Package>();
            int start = -1, end = -1;

            for (int i = 0; i < charArray.Length; i++)
            {
                //find index of opening and closing brackets and parse values in between

                if (charArray[i] == '(') start = i;
                if (charArray[i] == ')') end = i;

                if (start != -1 && end != -1)
                {
                    var package = packages.Substring(start + 1, end - start - 1).Split(',');
                    start = end = -1;

                    int index;
                    if (!int.TryParse(package[0], out index))
                        throw new ApiException("Error parsing package item , invalid value for Index");

                    double weight;
                    if (!double.TryParse(package[1], out weight))
                        throw new ApiException("Error parsing package item, invalid value for Weight");

                    int price;
                    if (!int.TryParse(package[2].Substring(1), out price))
                        throw new ApiException("Error parsing package item, invalid value for Price");

                    packageList.Add(new Package(index, weight, price));
                }
            }

            return new PackageLineItem(maxWeight, packageList);
        }

        private  void EnsureFileIsValid(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ApiException("File path cannot be null.");

            if (!Path.IsPathRooted(filePath))
                throw new ApiException("File path should be absolute.");

            if (!File.Exists(filePath))
                throw new ApiException($"File does not exist. invalid path: {filePath} ");
        }
    }
}
