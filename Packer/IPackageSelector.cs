using Packer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Packer
{
    public interface IPackageSelector
    {
        /// <summary>
        /// Takes a PackageLineItem and returns comma separated indexes of selected packages
        /// </summary>
        /// <param name="packageLineItem">package line item contains MaxWeight and collection of packages</param>
        /// <returns>selected packages</returns>
        string SelectPackages(PackageLineItem packageLineItem);
    }
}
