using Packer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Packer.Helpers
{
    /// <summary>
    ///  We could add a Generic interface like IParser<T>, but keeping things simple here
    /// </summary>
    public interface IParser
    {
        /// <summary>
        /// Takes a file path a returns a list of PackageLineItem
        /// </summary>
        /// <param name="filePath">path to the source file</param>
        /// <returns>List of parsed PackageLineItem</returns>
        List<PackageLineItem> Parse(string filePath);
    }
}
