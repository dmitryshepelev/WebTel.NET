using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTelNET.CommonNET.Libs.ExceptionResolvers
{
    public interface IExceptionResolver
    {
        string GetIdentifier(Exception ex);
    }
}
