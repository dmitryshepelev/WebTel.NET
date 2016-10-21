using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using Npgsql;

namespace WebTelNET.CommonNET.Resources
{
    public class DbResourceResolver : IResourceResolver
    {
        private readonly ResourceManager _resourceManager;

        public DbResourceResolver()
        {
            _resourceManager = new ResourceManager(typeof(DbResource));
        }

        public string ResolveException(Exception e)
        {
            var exceptionTyped = e as PostgresException;
            if (exceptionTyped != null)
            {
                return _resourceManager.GetString(exceptionTyped.SqlState);
            }
            return string.Empty;
        }
    }
}
