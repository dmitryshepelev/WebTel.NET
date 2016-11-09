using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;

namespace WebTelNET.CommonNET.Libs.ExceptionResolvers
{
    public class DbExceptionResolver : IExceptionResolver
    {
        public string GetIdentifier(Exception ex)
        {
            var exceptionTyped = ex as PostgresException;
            if (exceptionTyped != null)
            {
                return exceptionTyped.SqlState;
            }
            return string.Empty;
        }
    }
}
