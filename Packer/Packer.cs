using Packer.Helpers;
using Packer.Models;
using System.Collections.Generic;
using System.Text;
using Packer.Validator;
using Packer.Exceptions;

namespace Packer
{
    public static class Packer
    {
        public static string Pack(string filePath)
        {          
            IParser packageLineItemParser = new PackageLineItemParser();
            IPackageLineItemValidator packageLineItemValidator = new PackageLineItemValidator();
            IPackageSelector packageSelector = new PackageSelector();

            /*
            * parse the source file and return list of packages
            *  
            * 1. Get One Package (one line) at a time
            * 2. Validate all the conditions i.e. check weight, price etc
            * 3. Pick best combinations of packages
            * 
            */

            List<PackageLineItem> packageLineItems = packageLineItemParser.Parse(filePath);

            var packages = new StringBuilder();
            foreach(var packageLineItem in packageLineItems)
            {
                //validate package line item
                var (isValid, errorMessage) = packageLineItemValidator.Validate(packageLineItem);
                if (!isValid)
                    throw new ApiException(errorMessage);

                //select best combination of items
                string selectedPackages = packageSelector.SelectPackages(packageLineItem);
                packages.AppendLine(selectedPackages);
            }
            return packages.ToString();
        }                       
    }
}
