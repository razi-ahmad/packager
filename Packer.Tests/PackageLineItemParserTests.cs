using Packer.Exceptions;
using Packer.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Packer.Tests
{
    public class PackageLineItemParserTests
    {
        private readonly string _baseDirectory;
        private readonly IParser sut;
        public PackageLineItemParserTests()
        {
            _baseDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}Files";
            sut = new PackageLineItemParser();
        }
       
        [Fact]
        public void Throw_Exception_If_File_Path_Is_Null()
        {
            string path = null;            
            Assert.Throws<ApiException>(() => sut.Parse(path));
        }

        [Fact]
        public void Throw_Exception_If_File_Path_Is_Relative()
        {
            string path = "../fake_file_path";            
            Assert.Throws<ApiException>(() => sut.Parse(path));
        }

        [Fact]
        public void Throw_Exception_If_File_Not_Found()
        {
            string path = $"{_baseDirectory}\\not_found.txt";            
            Assert.Throws<ApiException>(() => sut.Parse(path));
        }

        [Fact]
        public void Throw_Exception_If_Max_Weight_Is_Non_Integer()
        {
            string path = $"{_baseDirectory}\\example_input_invalid_max_weight";            
            Assert.Throws<ApiException>(() => sut.Parse(path));
        }

        [Fact]
        public void Throw_Exception_If_Colon_Is_Missing_In_Line()
        {
            string path = $"{_baseDirectory}\\example_input_invalid_missing_colon";            
            Assert.Throws<ApiException>(() => sut.Parse(path));
        }

        [Fact]
        public void Throw_Exception_If_Index_Is_Non_Integer()
        {
            string path = $"{_baseDirectory}\\example_input_invalid_index";            
            Assert.Throws<ApiException>(() => sut.Parse(path));
        }

        [Fact]        
        public void Throw_Exception_If_Weight_Is_InValid()
        {
            string path = $"{_baseDirectory}\\example_input_invalid_weight";            
            Assert.Throws<ApiException>(() => sut.Parse(path));
        }

        [Fact]
        public void Throw_Exception_If_Price_Is_InValid()
        {
            string path = $"{_baseDirectory}\\example_input_invalid_price";            
            Assert.Throws<ApiException>(() => sut.Parse(path));
        }

       [Fact]
       public void Parse_Packages_If_Input_Valid()
        {
            string path = $"{_baseDirectory}\\example_input";
            IParser sut = new PackageLineItemParser();

            var packageLineItems = sut.Parse(path);

            Assert.Equal(4, packageLineItems.Count);

            //asserts weights
            Assert.Equal(81, packageLineItems[0].MaxWeight);
            Assert.Equal(8, packageLineItems[1].MaxWeight);
            Assert.Equal(75, packageLineItems[2].MaxWeight);
            Assert.Equal(56, packageLineItems[3].MaxWeight);
           
            //asserts items per package
            Assert.Equal(6, packageLineItems[0].Packages.Count);
            Assert.Single(packageLineItems[1].Packages);
            Assert.Equal(9, packageLineItems[2].Packages.Count);
            Assert.Equal(9, packageLineItems[3].Packages.Count);
        }

    }
}
