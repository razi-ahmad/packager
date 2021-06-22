using Packer.Config;
using Packer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Packer.Validator
{
    public class PackageLineItemValidator : IPackageLineItemValidator
    {        
        public (bool success, string errorMessage) Validate(PackageLineItem packageLineItem)
        {

            //validate max weight
            if (packageLineItem.MaxWeight > Constraints.PACKAGE_MAX_WEIGHT)
                return (
                    success : false,
                    errorMessage: $"Maxium weight for a package cannot exceed {Constraints.PACKAGE_MAX_WEIGHT}");

            //validate number of items
            if (packageLineItem.Packages.Count > Constraints.MAX_ITEMS_PER_PACKAGE)
                return (
                    success : false, 
                    errorMessage: $"Maximum items in a package cannot exceed {Constraints.MAX_ITEMS_PER_PACKAGE} items");

            //validate max weight and cost of each item
            foreach(var item in packageLineItem.Packages)
            {
                if (item.Weight > Constraints.MAX_WEIGHT_PER_ITEM)
                    return (
                            success: false,
                            errorMessage: $"Invalid weight for item at index {item.Index} , weight cannot exceed {Constraints.MAX_WEIGHT_PER_ITEM}");

                if(item.Price > Constraints.MAX_COST_PER_ITEM)
                    return (
                            success: false,
                            errorMessage: $"Invalid price for item at index {item.Index} , price cannot exceed {Constraints.MAX_COST_PER_ITEM}");
            }

            //if we reach this far , validations are passed
            return (success: true, errorMessage: null);
        }
    }
}
