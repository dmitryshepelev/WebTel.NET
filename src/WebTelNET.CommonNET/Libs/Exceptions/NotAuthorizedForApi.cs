using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTelNET.CommonNET.Libs.Exceptions
{
    public class NotAuthorizedForApi : Exception
    {
        public NotAuthorizedForApi()
        {
        }

        public NotAuthorizedForApi(string message) : base(message)
        {
        }

        public NotAuthorizedForApi(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
