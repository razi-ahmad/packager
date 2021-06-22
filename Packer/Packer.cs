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
