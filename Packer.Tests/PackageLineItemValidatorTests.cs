using Packer.Config;
using Packer.Models;
using Packer.Validator;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Packer.Tests
{
    public class PackageLineItemValidatorTests
    {
        private readonly IPackageLineItemValidator sut;
        public PackageLineItemValidatorTests()
        {
            sut = new PackageLineItemValidator();
        }
        [Fact]
        public void Return_Error_If_Max_Weight_Is_Greater_Than_Allowed_Weight()
        {
            var pkgLineItem = new PackageLineItem
            {
                MaxWeight = Constraints.PACKAGE_MAX_WEIGHT + 1, // max weight allowed is 100
                Packages = new List<Package>()
                {
                    new Package(1,2.0,45)
                }
            };
            
            var (success, message) = sut.Validate(pkgLineItem);

            Assert.False(success);
            Assert.Equal($"Maxium weight for a package cannot exceed {Constraints.PACKAGE_MAX_WEIGHT}", message);
        }

        [Fact]
        public void Return_Error_If_Items_In_Package_Are_Greater_Than_Allowed_Limit()
        {
            var pkgLineItem = new PackageLineItem
            {
                MaxWeight = 40,
                Packages = new List<Package>()
                {
                    new Package(1,2.0,45),
                    new Package(2,2.0,45),
                    new Package(3,2.0,45),
                    new Package(4,2.0,45),
                    new Package(5,2.0,45),
                    new Package(6,2.0,45),
                    new Package(7,2.0,45),
                    new Package(8,2.0,45),
                    new Package(9,2.0,45),
                    new Package(10,2.0,45),
                    new Package(11,2.0,45),
                    new Package(12,2.0,45),
                    new Package(13,2.0,45),
                    new Package(14,2.0,45),
                    new Package(15,2.0,45),
                    new Package(16,2.0,45),
                }
            };
            
            var (success, message) = sut.Validate(pkgLineItem);

            Assert.False(success);
            Assert.Equal($"Maximum items in a package cannot exceed {Constraints.MAX_ITEMS_PER_PACKAGE} items", message);
        }

        [Fact]
        public void Return_Error_If_Weight_Of_An_Item_Exceeds_Allowed_Limit()
        {
            int index = 1;
            var pkgLineItem = new PackageLineItem
            {
                MaxWeight = 50,
                Packages = new List<Package>()
                {
                    new Package(index,Constraints.MAX_WEIGHT_PER_ITEM + 1,45)
                }
            };
            
            var (success, message) = sut.Validate(pkgLineItem);

            Assert.False(success);
            
            Assert.Equal($"Invalid weight for item at index {index} , weight cannot exceed {Constraints.MAX_WEIGHT_PER_ITEM}", message);
        }

        [Fact]
        public void Return_Error_If_Cost_Of_An_Item_Exceeds_Allowed_Limit()
        {
            int index = 1;
            var pkgLineItem = new PackageLineItem
            {
                MaxWeight = 50,
                Packages = new List<Package>()
                {
                    new Package(index,50,Constraints.MAX_COST_PER_ITEM + 1)
                }
            };
            
            var (success, message) = sut.Validate(pkgLineItem);

            Assert.False(success);

            Assert.Equal($"Invalid price for item at index {index} , price cannot exceed {Constraints.MAX_COST_PER_ITEM}", message);
        }
    }
}
