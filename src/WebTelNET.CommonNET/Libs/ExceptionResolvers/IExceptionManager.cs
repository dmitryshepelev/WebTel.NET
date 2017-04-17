using System;
using WebTelNET.CommonNET.Resources;

namespace WebTelNET.CommonNET.Libs.ExceptionResolvers
{
    public interface IExceptionManager
    {
        Exception GetLastException(Exception ex);

        IExceptionResolver CreateResolver(Exception ex);
    }
}