using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTelNET.CommonNET.Libs.ExceptionResolvers
{
    public class DefaultExceptionResolver : IExceptionResolver
    {
        public string GetIdentifier(Exception ex)
        {
            return "DefaultError";
        }
    }
}
