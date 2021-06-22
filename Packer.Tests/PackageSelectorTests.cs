using Packer.Exceptions;
using Packer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Packer.Tests
{
    public class PackageSelectorTests
    {
        private readonly IPackageSelector sut;
        public PackageSelectorTests()
        {
            sut = new PackageSelector();
        }
        [Fact]
        public void Should_Select_Correct_Indexes_For_Selected_Packages()
        {
            var pkgLineItem = new PackageLineItem
            {
                MaxWeight = 81,
                Packages = new List<Package>
                {
                    new Package(2,88.62,98),
                    new Package(4,72.30,76),
                    new Package(6,46.34,48),
                    new Package(1,53.38,45),
                    new Package(5,30.18,9),
                    new Package(3,78.48,3)
                }
            };
            
            var result = sut.SelectPackages(pkgLineItem);

            Assert.Equal("4", result);
        }

        [Fact]
        public void Should_Return_Hyphen_If_No_Package_Selected()
        {
            var pkgLineItem = new PackageLineItem
            {
                MaxWeight = 8,
                Packages = new List<Package>
                {
                    new Package(1,15.3,98),                    
                }
            };
            
            var result = sut.SelectPackages(pkgLineItem);

            Assert.Equal("-", result);
        }

        
        [Fact]
        public void If_Two_Packages_Have_Same_Price_Should_Select_One_With_Least_Weight()
        {
            var pkgLineItem = new PackageLineItem
            {
                MaxWeight = 56,
                Packages = new List<Package>
                {
                    new Package(8,19.36,79), //same price
                    new Package(4,48.77,79), //same price
                    new Package(9,6.76,64),
                    new Package(7,81.80,45),
                    new Package(2,33.80,40),
                    new Package(5,46.81,36),
                    new Package(4,37.97,16),
                    new Package(1,90.72,13),
                    new Package(3,43.15,10),                    
                }
            };
            
            var result = sut.SelectPackages(pkgLineItem);

            Assert.Equal("8,9", result);

        }
    }
}
