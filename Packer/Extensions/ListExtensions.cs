using System;
using System.Collections.Generic;
using System.Text;

namespace Packer.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Returns a list of indexes (integers) as comma separated strings
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static string AsString(this List<int> items)
        {
            if (items.Count < 1)
                return "-";

            items.Sort();
            return string.Join(",", items);
        }
    }
}
