using System;
using System.Data.Common;

namespace WebTelNET.CommonNET.Libs.ExceptionResolvers
{
    public class ExceptionManager : IExceptionManager
    {
        public Exception GetLastException(Exception ex)
        {
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            return ex;
        }

        public IExceptionResolver CreateResolver(Exception ex)
        {
            if (ex is DbException)
            {
                return new DbExceptionResolver();
            }
            return new DefaultExceptionResolver();
        }
    }
}
