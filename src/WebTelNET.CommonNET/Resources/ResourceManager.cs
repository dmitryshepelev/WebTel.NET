using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTelNET.CommonNET.Resources;
using Npgsql;

namespace WebTelNET.CommonNET.Resources
{
    public class ResourceManager : IResourceManager
    {
        public virtual IDictionary<string, string> ResolveExeption(Exception e)
        {
            var key = "";
            var value = "";

            while (e != null)
            {
                var exTyped = e as PostgresException;
                if (exTyped != null)
                {
                    key = exTyped.SqlState;
                }
                value = e.Message;
                e = e.InnerException;
            }

            var dict = new Dictionary<string, string>();
            dict.Add(key, value);
            return dict;
        }
    }
}
