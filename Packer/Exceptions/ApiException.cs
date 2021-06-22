using System;
using System.Collections.Generic;
using System.Text;

namespace Packer.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(string error):base(error)
        {

        }
    }
}
