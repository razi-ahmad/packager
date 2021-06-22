using System;
using System.Collections.Generic;
using System.Text;

namespace Packer.Config
{
    public class Constraints
    {
        /// <summary>
        /// Maximum weight a package can take
        /// </summary>
        public const int PACKAGE_MAX_WEIGHT = 100;

        /// <summary>
        /// Max items per package to choose from
        /// </summary>
        public const int MAX_ITEMS_PER_PACKAGE = 15;

        /// <summary>
        /// Max weight an item in a package can have
        /// </summary>
        public const int MAX_WEIGHT_PER_ITEM = 100;

        /// <summary>
        /// Max cost an item in a package can have
        /// </summary>
        public const int MAX_COST_PER_ITEM = 100;
    }
}
