using Packer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Packer.Validator
{
    public interface IPackageLineItemValidator
    {
        (bool success , string errorMessage) Validate(PackageLineItem packageLineItem);
    }
}
